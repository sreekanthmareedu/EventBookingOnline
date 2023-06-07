using AutoMapper;
using BEventsWeb.Models;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess;
using BusinessEventsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventBooking.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class AdminController : Controller
    {
        private readonly IBEventService _eventService;
        private readonly IMapper mapper;

        public AdminController(IBEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            this.mapper = mapper;
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


        //=========================== Update Event =====================================

        public async Task<IActionResult> Update(int id)
        {
            var response = await _eventService.GetBEventsAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                BEventDTO res = JsonConvert.DeserializeObject<BEventDTO>(Convert.ToString(response.Results));
                return View(mapper.Map<BEventUpdateDTO>(res));
            }
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string SubmitButton, BEventUpdateDTO obj)
        {
            string buttonClicked = SubmitButton;
            if (buttonClicked == "Approve")
            {

                obj.eventStatus = "Approved";

            }
            if (buttonClicked == "Reject")
            {

                obj.eventStatus = "Rejected";

            }



            var response = await _eventService.UpdateBEventsAsync<APIResponse>(obj);

            if (response != null && response.IsSuccess)
            {

                return RedirectToAction("Index");
            }


            //}

            return View(obj);

        }

    }
}
