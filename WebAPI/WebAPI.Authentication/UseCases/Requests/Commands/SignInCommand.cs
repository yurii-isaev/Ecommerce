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
using WebAPI.Authentication.UseCases.Dto;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands
{
  /// <summary>
  /// Sets a property of the request object.
  /// </summary>
  public class SignInCommand : IRequest<Response>
  {
    public LoginDto? LoginDto { get; set; }
  }

  public class SignInCommandHandler : IRequestHandler<SignInCommand, Response>
  {
    readonly UserManager<User> _userManager;
    readonly SignInManager<User> _signInManager;
    readonly JwtOptions _jwtOptions;
    readonly IHttpContextAccessor _httpContextAccessor;

    public SignInCommandHandler
    (
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      IOptions<JwtOptions> jwtOptions,
      IHttpContextAccessor httpContextAccessor
    )
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _jwtOptions = jwtOptions.Value;
      _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Returns ResponseModel.</returns>
    public async Task<Response> Handle(SignInCommand request, CancellationToken token)
    {
      var httpContext = _httpContextAccessor.HttpContext;

      try
      {
        var userByEmail = await _userManager.FindByEmailAsync(request.LoginDto!.Email);

        if (userByEmail != null)
        {
          var result = await _signInManager.PasswordSignInAsync(
            userByEmail.UserName, request.LoginDto.Password, true, false
          );

          if (result.IsNotAllowed)
          {
            var appUser = await _userManager.FindByEmailAsync(request.LoginDto.Email);
            var role = await _userManager.GetRolesAsync(appUser);

            // Creating an object with a token and user data
            var user = new ProfileDto(
              appUser.FullName!, appUser.Email, appUser.UserName, appUser.CreatedAt, role.ElementAt(0));

            var jwtProvider = new JwtProvider(_jwtOptions, _userManager);
            user.Token = await jwtProvider.GenerateToken(appUser);

            // Adding data to browser cookies
            httpContext!.Response.Cookies.Append("user-cookies", user.Token);

            return await Task.FromResult(new Response(ResponseCode.Ok, true, Messages.TokenGenerated, user));
          }

          return await Task.FromResult(new Response(ResponseCode.Error, false, Messages.InvalidEmailOrPassword, ""));
        }
        else
        {
          throw new InvalidOperationException();
        }
      }
      catch (Exception ex)
      {
        return await Task.FromResult(new Response(ResponseCode.Error, ex.Message, Messages.UserNotExist));
      }
    }
  }
}
