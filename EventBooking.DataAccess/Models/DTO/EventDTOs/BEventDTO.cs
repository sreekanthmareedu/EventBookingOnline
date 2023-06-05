using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEventsAPI.Models
{
    public class BEventDTO
    {

        [Key]
        public int id { get; set; }
        [Required]
        
        public string eventName { get; set; }
     
        public string eventDescription { get; set; }
        [Required]
     
        public string location { get; set; }
        [Required]

        public DateTime eventDate { get; set; }
        [Required]
        public int availableSeats { get; set; }

       
        public string eventStatus { get; set; }

    }
}
