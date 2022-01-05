using System;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Authentication.Domain.Entities;

public class User : IdentityUser
{
   public string? FullName { get; set; } = null;
   
   public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
   
   public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
   
   public bool AcceptTerms { get; set; } = false;
}
