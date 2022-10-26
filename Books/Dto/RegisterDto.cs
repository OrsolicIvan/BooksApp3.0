﻿using System.ComponentModel.DataAnnotations;

namespace Books.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
