using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands;

public class ChangePasswordCommand : IRequest<ServerResponse>
{
  public CredentialDto? CredentialDto { get; set; }
}


/// <summary>
/// Implements a handler for change user password.
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ServerResponse>
{
  readonly UserManager<User> _userManager;

  public ChangePasswordCommandHandler(UserManager<User> userManager)
  {
    _userManager = userManager;
  }

  /// <summary> Handles a request.</summary>
  /// <returns> Returns Server Response. </returns>
  public async Task<ServerResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var email = request.CredentialDto!.Email;
      var password = request.CredentialDto.Password;
      var user = await _userManager.FindByEmailAsync(email);
      var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
      var resetResult = await _userManager.ResetPasswordAsync(user, Uri.UnescapeDataString(resetToken), password);

      if (resetResult.Succeeded)
      {
        return await Task.FromResult(new ServerResponse(200, true, Messages.PasswordChangedSuccess, ""));
      }

      return await Task.FromResult(new ServerResponse(500, false, Messages.PasswordChangedFailed, ""));
    }
    catch (Exception ex)
    {
      return await Task.FromResult(new ServerResponse(500, false, ex.Message, ""));
    }
  }
}
