using AutoMapper;
using BEventsWeb.Models;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess;
using BusinessEvents.DataAccess.Models;
using BusinessEventsAPI.Models;
using EventBooking.DataAccess.Repository.IRepository;
using EventBooking.DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace EventBooking.WebApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
  //  [Authorize(Roles = SD.Role_Customer)]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookingService _bookingService;

        private readonly IBEventService _eventService;
        private readonly IMapper mapper;

        public HomeController(IBEventService eventService, IBookingService bookingService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _eventService = eventService;
            _bookingService = bookingService;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> Index()
        {
            List<BEventDTO> List = new();
            var response = await _eventService.GetBEventsAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                List = JsonConvert.DeserializeObject<List<BEventDTO>>(Convert.ToString(response.Results));

            }


            return View(List);

        }

        public async Task<IActionResult> Home()
        {
            return View();
        }


//=====================================================================================================================================================================
//==================================================== My Bookings ====================================================================================================
public async Task<IActionResult> AllBookings()
        {

            List<BookingDTO> bookingInfo = new List<BookingDTO>();
            List<BEventDTO> BEventInfo = new List<BEventDTO>();

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var evnt = await _eventService.GetBEventsAsync<APIResponse>();

            if (evnt != null && evnt.IsSuccess)
            {
                BEventInfo= JsonConvert.DeserializeObject<List<BEventDTO>>(Convert.ToString(evnt.Results));
            }


            var response = await _bookingService.GetBookingAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
              
                bookingInfo = JsonConvert.DeserializeObject<List<BookingDTO>>(Convert.ToString(response.Results));
                
            }

       
            
           
            ViewBag.UserId = userId;
            var BookingViewModel = from s in bookingInfo
                                   join st in BEventInfo on s.EventId equals st.id into st2
                                   from st in st2.DefaultIfEmpty()
                                   select new BookingVM { bookingVM = s, bEventsVM = st };
            return View(BookingViewModel);

           
           

        }



//=======================================================================================================================================================================
//============================================ Book Tickets ==============================================================================================================

        public async Task<IActionResult> BookTicket(int id)
        {
           

            var response = await _eventService.GetBEventsAsync<APIResponse>(id);

           

         
                BEventDTO res = JsonConvert.DeserializeObject<BEventDTO>(Convert.ToString(response.Results));
                ViewBag.BEventInfo = res;
          



         
 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> BookTicket(BookingCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Seats", "Select Seats");
            }
            dto.EventId = (int)TempData["name"];
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.UserId = userId;


            var response = await _bookingService.CreateBookingASync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        //===========================================================================================================================================================================
        //======================================================================= Delete Booking ====================================================================================

        public async Task<IActionResult> DeleteBooking(int id)
        {


            var response = await _bookingService.DeleteBookingAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("AllBookings");
            }




            return View("Index");


          
        }

        //=========================================================================================================
        //===========================  Update Booking =============================================================

        public async Task<IActionResult> Update(int id)
        {
           
            var response = await _bookingService.GetBookingAsync<APIResponse>(id);

            BookingDTO res = JsonConvert.DeserializeObject<BookingDTO>(Convert.ToString(response.Results));

            var evntInfo = await _eventService.GetBEventsAsync<APIResponse>(id);

            BEventDTO evnt = JsonConvert.DeserializeObject<BEventDTO>(Convert.ToString(evntInfo.Results));
            ViewBag.BEventInfo = evnt;
            ViewBag.BookingInfo = res;

            return View(mapper.Map<BookingUpdateDTO>(res));
           

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookingUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Seats", "Select Seats");
            }
            dto.EventId = (int)TempData["name"];
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.UserId = userId;


            var response = await _bookingService.UpdateBookingAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("AllBookings");
            }

            return View();


        }




    }
}
