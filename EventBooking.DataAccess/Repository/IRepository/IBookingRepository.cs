using BusinessEvents.DataAccess.Models;
using BusinessEvents.DataAccess.Repository.IRepository;
using BusinessEventsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.DataAccess.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        public Task<Booking> UpdateAsync(Booking entity);

    }
}
