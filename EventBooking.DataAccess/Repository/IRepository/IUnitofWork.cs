using EventBooking.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEvents.DataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {
       public IBEventRepository BEvent { get; }

      public IBookingRepository Booking { get; }
        void Save();




    }
}
