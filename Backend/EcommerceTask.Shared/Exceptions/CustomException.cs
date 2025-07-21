using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Shared.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public CustomException() : base("An error occurred in the application.")
        {
        }
    }
}
