using static BusinessEvents.DataAccess.SD;

namespace BEventsWeb.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } 

        public string Url { get; set; }

        public object Data { get; set; }
    }
}
