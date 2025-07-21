using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.DTOs.Cart
{
    public record CartDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
