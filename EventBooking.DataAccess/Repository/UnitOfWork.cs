
using BusinessEvents.DataAccess.Repository.IRepository;
using EventBooking.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEvents.DataAccess.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;

        public IBEventRepository BEvent { get; private set; }

       

        public UnitOfWork(ApplicationDbContext db)
        {

            _db = db;
            BEvent = new BEventRepository(_db);
            
        }

        public void Save()
        {
          _db.SaveChanges();
        }
    }
}
