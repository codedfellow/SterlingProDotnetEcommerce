﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.DTOs.Authentication
{
    public record LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
