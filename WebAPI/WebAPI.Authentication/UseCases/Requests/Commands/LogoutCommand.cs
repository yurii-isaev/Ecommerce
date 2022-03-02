using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands;

public class LogoutCommand: IRequest<ServerResponse>
{
  public HttpContext HttpContext { get; }

  public LogoutCommand(HttpContext httpContext)
  {
    HttpContext = httpContext;
  }
}


public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ServerResponse>
{
  /// <summary> Handles a request.</summary>
  /// <returns>Returns Server Response.</returns>
  public Task<ServerResponse> Handle(LogoutCommand request, CancellationToken token)
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
      return Task.FromResult<ServerResponse>(new SuccessResponse(Messages.LogoutSuccess, null));
    }
    catch (Exception e)
    {
      // Return an error flag if an error occurred.
      return Task.FromResult<ServerResponse>(new InternalServerError(Messages.ServerError + e.Message));
    }
  }
}
