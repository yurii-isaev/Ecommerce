﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.UserCases.Common.Dto.Request
{
    /// <summary>
    /// Sets a properties of the data transfer object for user login.
    /// </summary>
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}