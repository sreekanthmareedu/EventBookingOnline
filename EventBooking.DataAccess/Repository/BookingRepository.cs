using BusinessEvents.DataAccess.Models;
using BusinessEvents.DataAccess.Repository;
using BusinessEventsAPI.Models;
using EventBooking.DataAccess.Data;
using EventBooking.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.DataAccess.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Booking> UpdateAsync(Booking entity)
        {

            _db.Update(entity);
            _db.SaveChanges();
            return entity;
        }

       
    }
}
