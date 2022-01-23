using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebAPI.Authentication.UseCases.Tranfers.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands;

public class LogoutUserCommand: IRequest<ServerResponse>
{
  public HttpContext HttpContext { get; }

  public LogoutUserCommand(HttpContext httpContext)
  {
    HttpContext = httpContext;
  }
}


public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, ServerResponse>
{
  /// <summary> Handles a request.</summary>
  /// <returns>Returns Server Response.</returns>
  public async Task<ServerResponse> Handle(LogoutUserCommand request, CancellationToken token)
  {
    var httpContext = request.HttpContext;

    try
    {
      // Set cookies with an expired JWT token.
      var cookieOptions = new CookieOptions
      {
        HttpOnly = true,
        Secure = true,
        // Set the date in the past.
        Expires = DateTime.UtcNow.AddDays(-1) 
      };
        
      // Applying the cookie setting.
      httpContext.Response.Cookies.Append(Messages.JwtCookiesKey, "", cookieOptions);
        
      // Return a success flag.
      return await Task.FromResult(new ServerResponse
      {
        Code = 200,
        Success = true,
        Message = "You've successfully logged out",
        DataSet = null
      });
    }
    catch (Exception ex)
    {
      // Return an error flag if an error occurred.
      return new InternalServerError("Internal server error: " + ex.Message);
    }
  }
}
