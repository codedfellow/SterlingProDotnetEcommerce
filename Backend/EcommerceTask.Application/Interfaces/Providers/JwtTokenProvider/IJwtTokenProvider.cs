using EcommerceTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.Interfaces.Providers.JwtTokenProvider
{
    public interface IJwtTokenProvider
    {
        string Create(User user);
    }
}
