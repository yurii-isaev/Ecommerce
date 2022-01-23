using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebAPI.Authentication.UseCases.Tranfers;
using WebAPI.Authentication.UseCases.Tranfers.Output;

namespace WebAPI.Authentication.UseCases.Requests.Queries;

public class GetAuthProfileQuery : IRequest<ServerResponse>
{
  public HttpContext HttpContext { get; }

  public GetAuthProfileQuery(HttpContext httpContext)
  {
    HttpContext = httpContext;
  }
}


public class GetAuthProfileQueryHandler : IRequestHandler<GetAuthProfileQuery, ServerResponse>
{
  /// <summary>
  /// Handles a request.
  /// </summary>
  /// <returns>Server Response.</returns>
  public async Task<ServerResponse> Handle(GetAuthProfileQuery request, CancellationToken token)
  {
    var httpContext = request.HttpContext;

    try
    {
      // Check if the user is authenticated.
      if (httpContext.User.Identity is ClaimsIdentity claimsIdentity && httpContext.User.Identity.IsAuthenticated)
      {
        // Create a list of required approvals.
        var claimsList = new List<(string Type, string Value)>
        {
          (ClaimTypes.NameIdentifier, claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value)!,
          (ClaimTypes.Name, claimsIdentity.FindFirst(ClaimTypes.Name)?.Value)!,
          (ClaimTypes.Email, claimsIdentity.FindFirst(ClaimTypes.Email)?.Value)!,
          ("jti", claimsIdentity.FindFirst("jti")?.Value)!,
          (ClaimTypes.Role, claimsIdentity.FindFirst(ClaimTypes.Role)?.Value)!
        };

        // Make sure that all statements are present and not empty.
        if (claimsList.All(c => !string.IsNullOrEmpty(c.Value)))
        {
          // Extract the values of the claims.
          var userId = claimsList.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
          var userName = claimsList.First(c => c.Type == ClaimTypes.Name).Value;
          var userEmail = claimsList.First(c => c.Type == ClaimTypes.Email).Value;
          var userRole = claimsList.First(c => c.Type == ClaimTypes.Role).Value;

          // Form a profile object for the response.
          var profile = new ProfileDto
          {
            Id = userId,
            UserName = userName,
            Email = userEmail,
            Role = userRole
          };

          // Return a successful response with the user's profile.
          return await Task.FromResult(new ServerResponse
          {
            Code = 200,
            Success = true,
            Message = "User is authenticated",
            DataSet = new {profile}
          });
        }
      }

      // If the user is not authenticated or has invalid authentication data, throw an exception.
      throw new AuthenticationException("User is not authenticated or has invalid authentication data");
    }
    catch (Exception ex)
    {
      // Handle other exceptions.
      return new InternalServerError("Internal server error: " + ex.Message);
    }
  }
}
