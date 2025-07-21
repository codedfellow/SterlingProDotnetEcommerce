using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.DTOs.Cart
{
    public record AddToCartDto
    {
        public Guid ProductId { get; set; }
    }
}
