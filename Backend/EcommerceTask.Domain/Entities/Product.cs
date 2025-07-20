using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Domain.Entities
{
    public sealed class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
