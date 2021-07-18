using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.Response
{
    public class ResponseResult<T> where T : new()
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Exception exception { get; set; }
        public List<T> data { get; set; }
    }
}
