using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEvents.DataAccess.Models
{
    public class Booking
    {

        public int BookingId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int TotalSeatsBooked { get; set; }    

        public string EventId { get; set; }

    }
}
