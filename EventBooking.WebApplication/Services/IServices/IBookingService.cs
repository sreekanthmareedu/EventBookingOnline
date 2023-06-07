using BusinessEvents.DataAccess.Models;
using BusinessEventsAPI.Models;

namespace BEventsWeb.Services.IServices
{
    public interface IBookingService
    {
        Task<T> GetBookingAsync<T>();

        Task<T> GetBookingAsync<T>(int id);

        Task<T> DeleteBookingAsync<T>(int id);

        Task<T> UpdateBookingAsync<T>(BookingUpdateDTO dto);

        Task<T> CreateBookingASync<T>(BookingCreateDTO dto);
    }
}
