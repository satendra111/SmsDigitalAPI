using AutoMapper;
using DAL.Entities;
using Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Automapper
{
    public class AutoMapperProfileDal : Profile
    {
        public AutoMapperProfileDal()
        {

            CreateMap<CityDetail, CityDetailEntityModel>()
                .ForMember(s => s.StartDate, d => d.MapFrom(s => s.start_date))
                .ForMember(s => s.EndDate, d => d.MapFrom(s => s.end_date));

            CreateMap<CityDetailEntityModel, CityDetail>()
                .ForMember(s => s.start_date, d => d.MapFrom(s => s.StartDate))
                .ForMember(s => s.end_date, d => d.MapFrom(s => s.EndDate));


        }
    }
}
