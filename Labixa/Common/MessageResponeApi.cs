using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labixa.Common
{
    public class MessageResponeApi
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class MessageResponeApiAdmin
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}