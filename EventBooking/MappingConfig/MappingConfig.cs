using AutoMapper;
using BusinessEvents.DataAccess.Models;
using BusinessEventsAPI.Models;

namespace BusinessEventsAPI.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<BEventDTO, BEvent>().ReverseMap();
            CreateMap<BEvent, BEventUpdateDTO>().ReverseMap();
            CreateMap<BEvent, BEventCreateDTO>().ReverseMap();


            CreateMap<BookingDTO, Booking>().ReverseMap();
            CreateMap<BookingDTO, BookingUpdateDTO>().ReverseMap();
            CreateMap<BookingDTO, BookingCreateDTO>().ReverseMap();
            CreateMap<Booking, BookingUpdateDTO>().ReverseMap();
            CreateMap<Booking, BookingCreateDTO>().ReverseMap();


        }
    }
}
