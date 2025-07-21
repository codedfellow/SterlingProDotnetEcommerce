using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.DTOs
{
    public class DefaultResponseModel
    {
        public DefaultResponseModel(){}
        public DefaultResponseModel(bool success, string message)
        {
            this.Success = success;
            this.Message = message; 
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
