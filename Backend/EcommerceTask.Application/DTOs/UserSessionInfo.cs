using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.DTOs
{
    public record UserSessionInfo
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
