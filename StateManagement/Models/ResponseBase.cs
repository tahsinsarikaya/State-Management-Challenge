using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Models
{
    public class ResponseBase
    {
        public bool Status { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
    }
}
