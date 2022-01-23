using System.Threading.Tasks;
using WebAPI.Authentication.Infrastructure.Options;

namespace WebAPI.Authentication.UseCases.Contracts;

public interface IEmailService
{
  Task SendEmail(string emailAddress, string content, EmailOptions options);
}
