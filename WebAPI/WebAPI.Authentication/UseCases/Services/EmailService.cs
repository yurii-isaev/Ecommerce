using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Serilog;
using WebAPI.Authentication.Infrastructure.Logging;
using WebAPI.Authentication.Infrastructure.Options;
using WebAPI.Authentication.UseCases.Contracts;

namespace WebAPI.Authentication.UseCases.Services;

public class EmailService : IEmailService
{
  /// <summary> Send an email. </summary>
  /// <sem name="address">Mailbox to send. </sem>
  /// <sem name="content">Email content.</sem>
  /// <return>Successfully completed task.</return>
  public async Task SendEmail(string emailAddress, string content, EmailOptions options)
  {
    try
    {
      var mail = new MailMessage();
      var smtp = new SmtpClient(options.Host);

      mail.From = new MailAddress(options.Secret!);
      mail.To.Add(emailAddress);
      mail.Subject = "Reset password";
      mail.Body = content;

      smtp.Port = options.Port;
      smtp.Credentials = new NetworkCredential(options.Secret, options.Key);
      smtp.EnableSsl = true;
      await smtp.SendMailAsync(mail);

      Log.Warning("The message was sent successfully.");
    }
    catch (Exception ex)
    {
      Logger.Fatal($"[Failed to send email]: {ex}");
      if (ex.InnerException != null)
      {
        Logger.Fatal($"Inner exception: {ex.InnerException}");
      }
      throw;
    }
  }
}
