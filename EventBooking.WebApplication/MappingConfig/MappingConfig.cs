

using AutoMapper;
using BusinessEvents.DataAccess.Models;
using BusinessEventsAPI.Models;

namespace BEventsWeb.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<BEventDTO, BEvent>().ReverseMap();
            CreateMap<BEventDTO, BEventUpdateDTO>().ReverseMap();
            CreateMap<BEventDTO, BEventCreateDTO>().ReverseMap();


            CreateMap<BookingDTO, Booking>().ReverseMap();
            CreateMap<BookingDTO, BookingUpdateDTO>().ReverseMap();
            CreateMap<BookingDTO, BookingCreateDTO>().ReverseMap();


        }
    }
}
