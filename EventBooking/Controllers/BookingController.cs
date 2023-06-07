using AutoMapper;
using BusinessEvents.DataAccess.Models;
using BusinessEvents.DataAccess.Repository;
using BusinessEvents.DataAccess.Repository.IRepository;
using BusinessEventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;
using System.Reflection;

namespace EventBooking.Controllers
{
    [ApiController]
    [Route("api/BookingAPI")]
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
        public async Task<ActionResult<APIResponses>> GetAllBookings()
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

        //====================================================================================================
        //================================= Get Booking Info ================================================
        [HttpGet("{id:int}", Name = "Booking")]

        public async Task<ActionResult<APIResponses>> GetSingleBooking (int id)
        {
            try
            {

                if (id <= 0)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }
                var evnt = await _unitOfWork.Booking.GetAsync(u => u.BookingId == id);

                if (evnt == null)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.NoContent;
                    return Ok(responses);

                }
                responses.Results = _mapper.Map<BookingDTO>(evnt);
                responses.StatusCode = HttpStatusCode.OK;
                return Ok(responses);

            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.Errors = new List<String> { ex.Message };
            }

            return responses;

        }
        //=========================================================================================================
        //=============================  Creaete Booking ==========================================================

        [HttpPost]
        public async Task<ActionResult<APIResponses>> CreateBooking([FromBody] BookingCreateDTO dto)

        {

            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);

                }

                if (dto == null)
                {
                    ModelState.AddModelError("Custom Error", "Invalid Details");
                    return BadRequest(ModelState);

                }




                var evntinfo = await _unitOfWork.BEvent.GetAsync(u => u.id == dto.EventId);
                if (evntinfo == null || (evntinfo.availableSeats < dto.TotalSeatsBooked))
                {
                    ModelState.AddModelError("Custom Error", "Insufficient Seats");
                    return BadRequest(ModelState);

                }


                Booking evnt = _mapper.Map<Booking>(dto);
                await _unitOfWork.Booking.CreateAsync(evnt);

                int UAvailableSeats = evntinfo.availableSeats - dto.TotalSeatsBooked;

                evntinfo.availableSeats = UAvailableSeats;

                await _unitOfWork.BEvent.UpdateAsync(evntinfo);

                _unitOfWork.Save();



                responses.Results = _mapper.Map<BookingDTO>(evnt);
                responses.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("Event", new { id = evnt.BookingId}, responses);
            }
            catch (Exception ex)
            {

                responses.IsSuccess = false;

                responses.Errors = new List<string> { ex.Message };
            }
            return responses;


        }

        //==========================================================================================================
        //================================== Delete Booking ========================================================

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponses>> RemoveBooking(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);
                }
                Booking evnt = await _unitOfWork.Booking.GetAsync(u => u.BookingId == Id);
                if (evnt == null)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);


                }
                await _unitOfWork.Booking.RemoveAsync(evnt);

                var evntinfo = await _unitOfWork.BEvent.GetAsync(u => u.id == evnt.EventId);

                int UAvailableSeats = evntinfo.availableSeats + evnt.TotalSeatsBooked;

                evntinfo.availableSeats = UAvailableSeats;

                await _unitOfWork.BEvent.UpdateAsync(evntinfo);




                _unitOfWork.Save();
                responses.IsSuccess = true;
                responses.StatusCode = HttpStatusCode.NoContent;
                //responses.Results = "Author with Id :" + Id + "  deleted successfully";
                return Ok(responses);
            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.Errors = new List<string> { ex.Message };
            }
            return responses;

        }

        //==========================================================================================================
        //====================================== Update Booking ====================================================

        [HttpPut("{id:int}")]

        public async Task<ActionResult<APIResponses>> UpdateBooking(int id, [FromBody] BookingUpdateDTO dto)
        {
            

            try
            {
                

                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);

                }

                if (id == null || (dto.BookingId != id))
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }


                Booking bookingInfo = await _unitOfWork.Booking.GetAsync(u => u.BookingId== id);
                if (bookingInfo.TotalSeatsBooked > dto.TotalSeatsBooked)
                {



                     var evntinfo = await _unitOfWork.BEvent.GetAsync(u => u.id == dto.EventId);
                    if (evntinfo == null || (evntinfo.availableSeats < dto.TotalSeatsBooked))
                    {
                        ModelState.AddModelError("Custom Error", "Insufficient Seats");
                        return BadRequest(ModelState);

                    }


                    
                  

                    int UAvailableSeats = evntinfo.availableSeats - dto.TotalSeatsBooked;

                    evntinfo.availableSeats = UAvailableSeats;


                    await _unitOfWork.BEvent.UpdateAsync(evntinfo);



                }
                else if(bookingInfo.TotalSeatsBooked < dto.TotalSeatsBooked)
                {
                     var evntinfo = await _unitOfWork.BEvent.GetAsync(u => u.id == dto.EventId);

                    int UAvailableSeats = evntinfo.availableSeats + dto.TotalSeatsBooked;

                    evntinfo.availableSeats = UAvailableSeats;

                    await _unitOfWork.BEvent.UpdateAsync(evntinfo);
                }
                 


               

                Booking evnt = _mapper.Map<Booking>(dto);

             
                await _unitOfWork.Booking.UpdateAsync(evnt);
                _unitOfWork.Save();
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Results = " Booking Updated successfully";
                return Ok(responses);


            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.Errors = new List<string> { ex.Message };
            }
            return responses;
        }



    }
}
