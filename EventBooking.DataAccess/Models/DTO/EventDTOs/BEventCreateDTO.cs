using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEventsAPI.Models
{
    public class BEventCreateDTO
    {

        
        [Required]
       
        [DisplayName("Event Name")]
        public string eventName { get; set; }



      
        [DisplayName("Event Description")]
        public string eventDescription { get; set; }



        [Required]
        [DisplayName("Location")]
       
        public string location { get; set; }

        [Required]
        [DisplayName("Event Date")]
        public DateTime eventDate { get; set; }


        [Required]
        [DisplayName("Available Seats")]
        public int availableSeats { get; set; }


    }
}
