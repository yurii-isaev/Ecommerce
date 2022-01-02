using Microsoft.Extensions.Options;

namespace WebAPI.Authentication.Infrastructure.Options;

public class SessionOptions : IOptions<SessionOptions>
{
   public SessionOptions Value { get; set; } = null!;
   public string? Cookie { get; set; }
   public string? Set { get; set; }
}
