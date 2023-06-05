using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEvents.DataAccess.Models
{
    public class APIResponses
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;

        public List<string> Errors { get; set; }

        public object Results { get; set; }
    }
}
