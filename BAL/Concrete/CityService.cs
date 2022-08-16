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
        /// <summary>
        /// CityService constructor
        /// </summary>
        /// <param name="cityRepository"></param>
        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }
        /// <summary>
        /// AddUpdateCityAsync
        /// </summary>
        /// <param name="cityDetailEntityModel"></param>
        /// <returns>Task<CityDetailEntityModel></returns>
        public async Task<CityDetailEntityModel> AddUpdateCityAsync(CityDetailEntityModel cityDetailEntityModel)
        {
           return await cityRepository.AddUpdateCityAsync(cityDetailEntityModel);
        }
        /// <summary>
        /// DeleteCityAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteCityAsync(int id)
        {
            return await cityRepository.DeleteCityAsync(id);
        }
        /// <summary>
        /// GetAllCityAsync
        /// </summary>
        /// <param name="cityDetailSearch"></param>
        /// <returns>Task<DataResult<CityDetailEntityModel>></returns>
        public async Task<DataResult<CityDetailEntityModel>> GetAllCityAsync(CityDetailSearch cityDetailSearch)
        {
            return await cityRepository.GetAllCityAsync(cityDetailSearch);
        }
        /// <summary>
        /// GetCityAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<CityDetailEntityModel></returns>
        public async Task<CityDetailEntityModel> GetCityAsync(int id)
        {
            return await cityRepository.GetCityAsync(id);
        }
    }
}
