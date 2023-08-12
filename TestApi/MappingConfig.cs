using AutoMapper;
using TestApi.Dto;
using TestApi.Models;

namespace TestApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Reservation,ReservationDTO>().ReverseMap();
        }
    }
}
