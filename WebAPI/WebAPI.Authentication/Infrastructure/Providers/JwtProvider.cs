using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.Infrastructure.Options;

namespace WebAPI.Authentication.Infrastructure.Providers;

public class JwtProvider
{
  readonly JwtOptions _jwtOptions;
  readonly UserManager<User> _userManager;

  public JwtProvider(JwtOptions jwtOptions, UserManager<User> userManager)
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

    if (_jwtOptions == null)
    {
      throw new ArgumentException("JWT options are not set");
    }

    if (string.IsNullOrEmpty(_jwtOptions.Secret))
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

    var claims = new List<Claim>()
    {
      new Claim(JwtRegisteredClaimNames.NameId, user.Id),
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    foreach (var role in userRoles)
    {
      claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var jwtTokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddHours(_jwtOptions.TokenLifeTime),
      Audience = _jwtOptions.Audience,
      Issuer = _jwtOptions.Issuer,
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
    };

    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    return jwtTokenHandler.WriteToken(token);
  }
}
