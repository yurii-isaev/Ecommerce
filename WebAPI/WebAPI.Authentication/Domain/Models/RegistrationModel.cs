using System.ComponentModel.DataAnnotations;

namespace WebAPI.Authentication.Domain.Models
{
   public class RegistrationModel
   {
      public string? FullName { get; set; }

      [Required] public string? Username { get; set; }

      [Required] public string? Password { get; set; }

      [Required] public string? Email { get; set; }
   }
}
