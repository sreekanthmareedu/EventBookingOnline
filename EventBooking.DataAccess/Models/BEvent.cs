using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEventsAPI.Models
{
    public class BEvent
    {

        [Key]
        public int id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string eventName { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string eventDescription { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string location { get; set; }
        [Required]

        public DateTime eventDate { get; set; }
        [Required]
        
        public int availableSeats { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string eventStatus { get; set; }

    }
}
