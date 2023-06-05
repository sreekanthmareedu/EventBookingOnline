using BusinessEventsAPI.Models;

namespace BEventsWeb.Services.IServices
{
    public interface IBEventService
    {
        Task<T> GetBEventsAsync<T>();

        Task<T> GetBEventsAsync<T>(int id);

        Task<T> DeleteBEventsAsync<T>(int id);

        Task<T> UpdateBEventsAsync<T>(BEventUpdateDTO dto);

        Task<T> CreateBEventsASync<T>(BEventCreateDTO dto);
    }
}
