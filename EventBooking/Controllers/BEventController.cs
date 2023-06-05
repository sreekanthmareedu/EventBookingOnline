using AutoMapper;
using Azure;
using BusinessEvents.DataAccess.Models;
using BusinessEvents.DataAccess.Repository.IRepository;
using BusinessEventsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace BusinessEventsAPI.Controllers
{
    [ApiController]
    [Route("api/EventAPI")]
    public class BEventController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        protected APIResponses responses;
        private readonly IMapper _mapper;

        public BEventController(IUnitofWork unitofWork, IMapper mapper)
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
                

                IEnumerable<BEventDTO> res = _mapper.Map<List<BEventDTO>>(await _unitOfWork.BEvent.GetAllAsync());
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

        //==========================================================================
        //======================== Get Single Event ================================


        [HttpGet("{id:int}", Name = "Event")]
        
        public async Task<ActionResult<APIResponses>> GetEvent(int id)
        {
            try
            {

                if (id <= 0)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }
                var evnt = await _unitOfWork.BEvent.GetAsync(u => u.id == id);

                if (evnt == null)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.NoContent;
                    return Ok(responses);

                }
                responses.Results = _mapper.Map<BEventDTO>(evnt);
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

        //==============================================================================
        //======================== Create Event ========================================

        [HttpPost]
        public async Task<ActionResult<APIResponses>> CreateEvent([FromBody] BEventCreateDTO dto)

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
               
                BEvent evnt = _mapper.Map<BEvent>(dto);
                evnt.eventStatus = "Pending";
                await _unitOfWork.BEvent.CreateAsync(evnt);
                 _unitOfWork.Save();
                responses.Results = _mapper.Map<BEventDTO>(evnt);
                responses.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("Event", new { id = evnt.id }, responses);
            }
            catch (Exception ex)
            {

                responses.IsSuccess = false;

                responses.Errors = new List<string> { ex.Message };
            }
            return responses;


        }

        //==============================================================================
        //======================= Delete Event =========================================

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponses>> RemoveEvent(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);
                }
                BEvent evnt = await _unitOfWork.BEvent.GetAsync(u => u.id == Id);
                if (evnt == null)
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);


                }
                await _unitOfWork.BEvent.RemoveAsync(evnt);
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

        //===============================================================================
        //===================== Update Event ============================================


        [HttpPut("{id:int}")]
        
        public async Task<ActionResult<APIResponses>> UpdateEvent(int id, [FromBody] BEventUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);

                }

                if (id == null || (dto.id != id))
                {
                    responses.Results = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }

            

                BEvent evnt = _mapper.Map<BEvent>(dto);
                
                if(evnt.eventStatus == null) 
                {
                    evnt.eventStatus = "Pending";
                }
                await _unitOfWork.BEvent.UpdateAsync(evnt);
                _unitOfWork.Save();
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Results = " Event Updated successfully";
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
