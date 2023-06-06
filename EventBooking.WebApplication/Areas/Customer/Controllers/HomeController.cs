using AutoMapper;
using BEventsWeb.Models;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess;
using BusinessEventsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace EventBooking.WebApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = SD.Role_Customer)]
    public class HomeController : Controller
    {
        private readonly IBEventService _eventService;
        private readonly IMapper mapper;

        public HomeController(IBEventService eventService, IMapper mapper)
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

        public async Task<IActionResult> Home()
        {
            return View();
        }

    }
}
