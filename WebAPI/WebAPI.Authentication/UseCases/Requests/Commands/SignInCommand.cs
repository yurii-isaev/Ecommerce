using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.Infrastructure.Options;
using WebAPI.Authentication.Infrastructure.Providers;
using WebAPI.Authentication.UseCases.Models;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands;

/// <summary>
/// Sets a property of the request object.
/// </summary>
public class SignInCommand : IRequest<ServerResponse>
{
  public LoginDto? LoginDto { get; set; }
}


public class SignInCommandHandler : IRequestHandler<SignInCommand, ServerResponse>
{
  readonly IHttpContextAccessor _httpContextAccessor;
  readonly IOptions<JwtOptions> _jwtOptions;
  readonly UserManager<User> _userManager;
  readonly SignInManager<User> _signInManager;

  public SignInCommandHandler
  (
    IHttpContextAccessor httpContextAccessor,
    IOptions<JwtOptions> jwtOptions,
    SignInManager<User> signInManager,
    UserManager<User> userManager
  )
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _jwtOptions = jwtOptions;
    _httpContextAccessor = httpContextAccessor;
  }

  /// <summary> Handles a request. </summary>
  /// <param name="request">The request.</param>
  /// <param name="token">Cancellation token.</param>
  /// <returns>Server Response.</returns>
  public async Task<ServerResponse> Handle(SignInCommand request, CancellationToken token)
  {
    var httpContext = _httpContextAccessor.HttpContext;

    try
    {
      var user = await _userManager.FindByEmailAsync(request.LoginDto!.Email);

      if (user != null)
      {
        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.LoginDto.Password, true, false);

        if (result.IsNotAllowed)
        {
          // var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
          var roles = await _userManager.GetRolesAsync(user);

          // Creating an object with a token and user data.
          var profile = new ProfileDto(user.Id, user.UserName, user.CreatedAt, user.Email, roles.ElementAt(0));

          // Generate jwt by provider.
          var jwtProvider = new JwtProvider(_jwtOptions, _userManager);
          var jwt = await jwtProvider.GenerateToken(user);

          // Adding data to browser cookies.
          httpContext!.Response.Cookies.Append(Messages.JwtCookiesKey, jwt, new CookieOptions
          {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
          });
          
          return new SuccessResponse(Messages.TokenGenerated, profile);
        }

        return new InternalServerError(Messages.InvalidEmailOrPassword);
      }
      else
      {
        throw new InvalidOperationException();
      }
    }
    catch (Exception ex)
    {
      return new InternalServerError(Messages.UserNotExist + ex.Message);
    }
  }
}
