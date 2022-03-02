using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.Infrastructure.Options;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands;

public class ForgotPasswordCommand : IRequest<ServerResponse>
{
  public CredentialDto? CredentialDto { get; set; }
}


public class ForgotPasswordCommandHandler : Controller, IRequestHandler<ForgotPasswordCommand, ServerResponse>
{
  readonly IEmailService _emailService;
  readonly IOptions<EmailOptions> _emailOptions;
  readonly UserManager<User> _userManager;

  public ForgotPasswordCommandHandler
  (
    IEmailService emailService,
    IOptions<EmailOptions> emailOptions,
    UserManager<User> userManager
  )
  {
    _emailService = emailService;
    _emailOptions = emailOptions;
    _userManager = userManager;
  }

  /// <summary> Handles a request. </summary>
  /// <returns> Returns string about success. </returns>
  public async Task<ServerResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var currentUser = await _userManager.FindByEmailAsync(request.CredentialDto!.Email);

      if (currentUser != null || ((User)null!).EmailConfirmed)
      {
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(currentUser!);
        var url = "http://localhost:8080/change-password";
        var emailLink = await GenerateEmailLink(request, resetToken, url, currentUser!);

        await _emailService.SendEmail(request.CredentialDto.Email!, emailLink, _emailOptions.Value);
        
        return new SuccessResponse(Messages.MailCollectLink, null);
      } 

      return new InternalServerError(Messages.SendMessageFailed);
    }
    catch (Exception e)
    {
      return new InternalServerError(Messages.ServerError + e.Message);
    }
  }

  /// <summary> Generate email link. </summary>
  /// <param name="request"></param>
  /// <param name="token">Access token.</param>
  /// <param name="header"></param>
  /// <param name="user">User entity.</param>
  /// <returns>Return token.</returns>
  public Task<string> GenerateEmailLink(ForgotPasswordCommand request, string token, StringValues header, User user)
  {
    var uriBuilder = new UriBuilder(header);
    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
    query["token"] = token;
    query["userid"] = user.Id;
    // uriBuilder.Query = "http://localhost:8080/#/change-password";
    // uriBuilder.Query = request.AccountDto!.ResetPasswordUri!;

    var recoveryLink = uriBuilder.ToString();
    var emailBody = $"Click on link to change password {recoveryLink}";
    return Task.FromResult(emailBody);
  }
}
