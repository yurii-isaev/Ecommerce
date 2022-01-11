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
using WebAPI.Authentication.UseCases.Tranfers;
using WebAPI.Authentication.UseCases.Tranfers.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands
{
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

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Returns response model.</returns>
    public async Task<ServerResponse> Handle(SignInCommand request, CancellationToken token)
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
            var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
            var rolesList = await _userManager.GetRolesAsync(user);

            // Creating an object with a token and user data
            var profile = new ProfileDto(
              new Guid(user.Id), user.UserName, user.CreatedAt, user.Email, rolesList.ElementAt(0)
            );
            //
            var jwtProvider = new JwtProvider(_jwtOptions, _userManager);
            var jwt = await jwtProvider.GenerateToken(user);

            // Adding data to browser cookies
            httpContext!.Response.Cookies.Append("user-cookies", jwt);

            return await Task.FromResult(new ServerResponse(ResponseCode.Ok, true, Messages.TokenGenerated, profile));
          }

          return await Task.FromResult(new ServerResponse(ResponseCode.Error, false, Messages.InvalidEmailOrPassword, ""));
        }
        else
        {
          throw new InvalidOperationException();
        }
      }
      catch (Exception ex)
      {
        return await Task.FromResult(new ServerResponse(ResponseCode.Error, ex.Message, Messages.UserNotExist));
      }
    }
  }
}
