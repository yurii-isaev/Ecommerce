using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Models;

namespace WebAPI.Authentication.UseCases.Requests.Queries
{
  /// <summary>
  /// Sets a property of the request object.
  /// </summary>
  public class GetUserListQuery : IRequest<IEnumerable>
  {}

  /// <summary>
  /// Implements a request handler for a list of registered users.
  /// </summary>
  public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable>
  {
    readonly UserManager<User> _userManager;

    public GetUserListQueryHandler(UserManager<User> manager) => _userManager = manager;

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Returns registration user list.</returns>
    public async Task<IEnumerable> Handle(GetUserListQuery request, CancellationToken token)
    {
      var profiles = new List<ProfileDto>();
      var users = _userManager.Users;

      foreach (var user in users)
      {
        var roles = await _userManager.GetRolesAsync(user);
        profiles.Add(new ProfileDto(user.Id, user.UserName, user.CreatedAt, user.Email, roles.FirstOrDefault()!));
      }

      return await Task.FromResult(profiles);
    }
  }
}
