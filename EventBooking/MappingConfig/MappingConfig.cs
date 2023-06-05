using AutoMapper;
using BusinessEventsAPI.Models;

namespace BusinessEventsAPI.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<BEventDTO, BEvent>().ReverseMap();
            CreateMap<BEvent, BEventUpdateDTO>().ReverseMap();
            CreateMap<BEvent, BEventCreateDTO>().ReverseMap();


        }
    }
}
