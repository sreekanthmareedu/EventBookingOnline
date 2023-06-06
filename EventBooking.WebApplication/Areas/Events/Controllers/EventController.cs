using AutoMapper;
using BEventsWeb.Models;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess;
using BusinessEventsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace EventBooking.WebApplication.Areas.Events.Controllers
{
    [Area("Events")]
    [Authorize(Roles = SD.Role_Employee)]
    public class EventController : Controller
    {
        private readonly IBEventService _eventService;
        private readonly IMapper mapper;

        public EventController(IBEventService eventService, IMapper mapper)
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

        //=========================================================================================================
        //=============================== Create Event ============================================================



        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BEventCreateDTO obj)
        {
            if (ModelState.IsValid)
            {
                var response = await _eventService.CreateBEventsASync<APIResponse>(obj);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }


            }

            return View(obj);

        }
        //=============================================================================
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
        public async Task<IActionResult> Update(BEventUpdateDTO obj)
        {
            obj.eventStatus = "Pending";
           /* if (ModelState.IsValid)
            {*/
                var response = await _eventService.UpdateBEventsAsync<APIResponse>(obj);

                if (response != null && response.IsSuccess)
                {

                    return RedirectToAction("Index");
                }


           /* }*/

            return View(obj);

        }
        //=============================================================================
        //========================== Delete Event =====================================

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _eventService.GetBEventsAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                BEventDTO res = JsonConvert.DeserializeObject<BEventDTO>(Convert.ToString(response.Results));
                return View(res);
            }
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BEventDTO res)
        {

            var response = await _eventService.DeleteBEventsAsync<APIResponse>(res.id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("Index");
            }




            return View(res);

        }
    }
}
