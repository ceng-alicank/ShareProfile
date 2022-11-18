using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{

    public class Response<T>
    {
        public Response(bool isSucceeded)
        {
            IsSucceeded = isSucceeded;
        }
        public Response(T data)
        {
            Data = data;
            IsSucceeded = true;
        }
        public Response(string message)
        {
            Message = message;
            IsSucceeded = false;
        }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool IsSucceeded { get; set; }

    }
}
