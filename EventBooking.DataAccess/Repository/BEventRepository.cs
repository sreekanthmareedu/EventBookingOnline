
using BusinessEvents.DataAccess.Repository.IRepository;
using BusinessEventsAPI.Models;
using EventBooking.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BusinessEvents.DataAccess.Repository
{
    public class BEventRepository : Repository<BEvent>,IBEventRepository 
    {
        private ApplicationDbContext _db;
        public BEventRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<BEvent> UpdateAsync(BEvent entity)
        {

            _db.Update(entity);
           _db.SaveChanges();
            return entity;
        }
    }
}
