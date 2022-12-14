using Domain.CommonEntity;
using Domain.EntityModel;
using Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICityRepository
    {
        Task<CityDetailEntityModel> AddUpdateCityAsync(CityDetailEntityModel cityDetailEntityModel);
        Task<CityDetailEntityModel> GetCityAsync(int id);
        Task<DataResult<CityDetailEntityModel>> GetAllCityAsync(CityDetailSearch cityDetailSearch);
        Task<bool> DeleteCityAsync(int id);

    }
}
