﻿using BusinessEventsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEvents.DataAccess.Models
{
    public class BookingCreateDTO
    {
        
        
        required
        public int TotalSeatsBooked { get; set; }    


        public int EventId { get; set; }

      
        public string UserId { get; set; }

  


    }
}
