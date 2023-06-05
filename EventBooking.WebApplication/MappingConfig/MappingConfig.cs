

using AutoMapper;
using BusinessEventsAPI.Models;

namespace BEventsWeb.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<BEventDTO, BEvent>().ReverseMap();
            CreateMap<BEventDTO, BEventUpdateDTO>().ReverseMap();
            CreateMap<BEventDTO, BEventCreateDTO>().ReverseMap();


        }
    }
}
