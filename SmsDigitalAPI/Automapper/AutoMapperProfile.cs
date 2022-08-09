using API.Model;
using AutoMapper;
using Domain.EntityModel;
using Domain.Search;

namespace API.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CityModel, CityDetailEntityModel>();
            CreateMap<CitySearchModel, CityDetailSearch>();
        }
    }
}
