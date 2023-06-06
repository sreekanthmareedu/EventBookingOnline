using AutoMapper;
using BusinessEvents.DataAccess.Models;
using BusinessEvents.DataAccess.Repository.IRepository;
using BusinessEventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventBooking.Controllers
{
    public class BookingController : Controller
    {

        private readonly IUnitofWork _unitOfWork;
        protected APIResponses responses;
        private readonly IMapper _mapper;

        public BookingController(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitOfWork = unitofWork;
            _mapper = mapper;
            responses = new APIResponses();

        }

        [HttpGet]
        public async Task<ActionResult<APIResponses>> GetAll()
        {
            try
            {


                IEnumerable<BookingDTO> res = _mapper.Map<List<BookingDTO>>(await _unitOfWork.Booking.GetAllAsync());
                responses.StatusCode = HttpStatusCode.OK;
                responses.Results = res;
                return Ok(responses);
            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.Errors =
                    new List<string> { ex.Message };

            }
            return responses;

        }

    }
}
