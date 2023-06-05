using BEventsWeb.Models;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace BEventsWeb.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse response { get ; set ; }

        public IHttpClientFactory httpclient { get ; set ; }    

        public BaseService(IHttpClientFactory httpclient)
        {

            this.response = new();
            this.httpclient = httpclient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpclient.CreateClient("BusinessEventsAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                                           Encoding.UTF8, "application/json"

                        );

                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                }
                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }


            catch (Exception ex)
            {


                var dto = new APIResponse
                {
                    Errors = new List<string> { Convert.ToString(ex) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;


            }
            
        }
    }
}
