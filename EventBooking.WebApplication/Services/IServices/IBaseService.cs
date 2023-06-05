

using BEventsWeb.Models;

namespace BEventsWeb.Services.IServices
{
    public interface IBaseService
    {
        APIResponse response { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
