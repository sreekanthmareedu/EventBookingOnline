
using BEventsWeb.Models;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess;
using BusinessEventsAPI.Models;

namespace BEventsWeb.Services
{
    public class BEventService : BaseService, IBEventService
    {
        private readonly IHttpClientFactory _ClientFactory;

        private string Eventurl;
        public BEventService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _ClientFactory = clientFactory;
            Eventurl = configuration.GetValue<String>("ServiceUrls:EventAPI");
        }

        public Task<T> CreateBEventsASync<T>(BEventCreateDTO dto)
        {

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = Eventurl + "/api/EventAPI"

            });
        }

        

        public Task<T> DeleteBEventsAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,

                Url = Eventurl + "/api/EventAPI/" + id

            });
        }

        public Task<T> GetBEventsAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = Eventurl + "/api/EventAPI"

            });
        }

        public Task<T> GetBEventsAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = Eventurl + "/api/EventAPI/" + id

            });
        }

        public Task<T> UpdateBEventsAsync<T>(BEventUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = Eventurl + "/api/EventAPI/" + dto.id

            });
        }
    }
}
