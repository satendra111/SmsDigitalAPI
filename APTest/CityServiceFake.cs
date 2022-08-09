using BAL.Abstract;
using Domain.CommonEntity;
using Domain.EntityModel;
using Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APTest
{
    public class CityServiceFake : ICityService
    {
        private readonly DataResult<CityDetailEntityModel> dataResult;
        public CityServiceFake()
        {
            dataResult = new DataResult<CityDetailEntityModel>()
            {
                list = new List<CityDetailEntityModel>()
                {
                    new CityDetailEntityModel(){Id=1,City="Neftegorsk",StartDate=DateTime.Now.Date,EndDate=DateTime.Now.Date.AddDays(10), Price = 55.82, Status = "Seldom", Color = "#fd4e19"},
                    new CityDetailEntityModel(){Id=1,City="Test",StartDate=DateTime.Now.Date,EndDate=DateTime.Now.Date.AddDays(10), Price = 55.82, Status = "Seldom", Color = "#fd4e21"},
                },
                totalCount = 2,
            };
        }
        public async Task<CityDetailEntityModel> AddUpdateCityAsync(CityDetailEntityModel cityDetailEntityModel)
        {
            if (cityDetailEntityModel.Id == 0)
            {
                dataResult.list.Add(cityDetailEntityModel);
            }
            else
            {
                var city = dataResult.list.Where(s=>s.Id==cityDetailEntityModel.Id).FirstOrDefault();
                dataResult.list.Remove(city);
                dataResult.list.Add(cityDetailEntityModel);
            }
            return cityDetailEntityModel;
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var city = dataResult.list.Where(s => s.Id == id).FirstOrDefault();
            dataResult.list.Remove(city);
            return true;
            
        }

        public async Task<DataResult<CityDetailEntityModel>> GetAllCityAsync(CityDetailSearch cityDetailSearch)
        {
            return dataResult;
        }

        public async Task<CityDetailEntityModel> GetCityAsync(int id)
        {
            return dataResult.list.Where(s=>s.Id == id).FirstOrDefault();
        }
    }
}
