using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.DTOs.Authentication
{
    public record UserRegistrationDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(3)]
        public string UserName { get; set; }
        [MinLength(5)]
        public string Password { get; set; }
    }
}
