using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.Infrastructure.Options;

namespace WebAPI.Authentication.Infrastructure.Providers;

public class JwtProvider
{
  readonly IOptions<JwtOptions> _jwtOptions;
  readonly UserManager<User> _userManager;

  public JwtProvider(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager)
  {
    _jwtOptions = jwtOptions;
    _userManager = userManager;
  }

  public async Task<string> GenerateToken(User user)
  {
    if (user == null)
    {
      throw new ArgumentException("User object is null");
    }

    if (string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.Email))
    {
      throw new ArgumentException("User properties are null or empty");
    }

    if (_jwtOptions.Value == null)
    {
      throw new ArgumentException("JWT options are not set");
    }

    if (string.IsNullOrEmpty(_jwtOptions.Value.Secret))
    {
      throw new ArgumentException("JWT Secret is not set");
    }

    var userId = await _userManager.FindByIdAsync(user.Id);
    if (userId == null)
    {
      throw new Exception("User not found");
    }

    var userRoles = await _userManager.GetRolesAsync(userId);
    if (userRoles == null)
    {
      throw new Exception("User roles not found");
    }

    var jwtClaimsList = new List<Claim>()
    {
      new Claim(JwtRegisteredClaimNames.NameId, user.Id),
      new Claim(ClaimTypes.Name, user.UserName),
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    foreach (var role in userRoles)
    {
      jwtClaimsList.Add(new Claim(ClaimTypes.Role, role));
    }

    var jwtTokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Secret);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(jwtClaimsList),
      Expires = DateTime.UtcNow.AddHours(_jwtOptions.Value.TokenLifeTime),
      Audience = _jwtOptions.Value.Audience,
      Issuer = _jwtOptions.Value.Issuer,
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
    };

    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    return jwtTokenHandler.WriteToken(token);
  }
}