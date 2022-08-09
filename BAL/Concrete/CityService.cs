using BAL.Abstract;
using DAL.Abstract;
using Domain.CommonEntity;
using Domain.EntityModel;
using Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concrete
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }
        public async Task<CityDetailEntityModel> AddUpdateCityAsync(CityDetailEntityModel cityDetailEntityModel)
        {
           return await cityRepository.AddUpdateCityAsync(cityDetailEntityModel);
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            return await cityRepository.DeleteCityAsync(id);
        }

        public async Task<DataResult<CityDetailEntityModel>> GetAllCityAsync(CityDetailSearch cityDetailSearch)
        {
            return await cityRepository.GetAllCityAsync(cityDetailSearch);
        }

        public async Task<CityDetailEntityModel> GetCityAsync(int id)
        {
            return await cityRepository.GetCityAsync(id);
        }
    }
}
