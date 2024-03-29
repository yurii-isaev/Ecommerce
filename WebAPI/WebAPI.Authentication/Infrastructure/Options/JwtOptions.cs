using Microsoft.Extensions.Options;

namespace WebAPI.Authentication.Infrastructure.Options;

public class JwtOptions : IOptions<JwtOptions>
{
   public JwtOptions Value { get; set; } = null!;
   public string? Secret { get; set; }
   public string? Issuer { get; set; }
   public string? Audience { get; set; }
   public int TokenLifeTime { get; set; }
}
