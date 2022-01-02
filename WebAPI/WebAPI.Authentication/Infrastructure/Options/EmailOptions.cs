using Microsoft.Extensions.Options;

namespace WebAPI.Authentication.Infrastructure.Options;

public class EmailOptions : IOptions<EmailOptions>
{
   public EmailOptions Value { get; set; } = null!;
   public string? Host { get; set; }
   public int Port { get; set; }
   public string? Secret { get; set; }
   public string? Key { get; set; }
}