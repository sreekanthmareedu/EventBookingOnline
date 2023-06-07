
using BEventsWeb.Models;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess;
using BusinessEvents.DataAccess.Models;
using BusinessEventsAPI.Models;

namespace BEventsWeb.Services
{
    public class BookingService : BaseService, IBookingService
    {
        private readonly IHttpClientFactory _ClientFactory;

        private string Eventurl;
        public BookingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _ClientFactory = clientFactory;
            Eventurl = configuration.GetValue<String>("ServiceUrls:EventAPI");
        }

        public Task<T> CreateBookingASync<T>(BookingCreateDTO dto)
        {

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = Eventurl + "/api/BookingAPI"

            });
        }

    

        public Task<T> DeleteBookingAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,

                Url = Eventurl + "/api/BookingAPI/" + id

            });
        }

        public Task<T> GetBookingAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = Eventurl + "/api/BookingAPI"

            });
        }

        public Task<T> GetBookingAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = Eventurl + "/api/BookingAPI/" + id

            });
        }

        public Task<T> UpdateBookingAsync<T>(BookingUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = Eventurl + "/api/BookingAPI/" + dto.BookingId

            });
        }

     
    }
}
