using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Models
{
    public class EventArgsError
    {
        public string message { get; set; }
        public EventArgsError(string mess) => message = mess;
    }
}