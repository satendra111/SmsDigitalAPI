using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Concrete;
using DAL.Abstract;
using DAL.DbContexts;
using DAL.Entities;
using Domain.CommonEntity;
using Domain.EntityModel;
using Domain.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class CityRepository : ICityRepository
    {
        private ApiDbContext dbContext;
        private readonly IMapper mapper;
        private ICustomPaging<CityDetailEntityModel> customPaging;
        public CityRepository(ApiDbContext context, IMapper mapper, ICustomPaging<CityDetailEntityModel> customPaging)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.customPaging = customPaging;

        }
        public async Task<CityDetailEntityModel> AddUpdateCityAsync(CityDetailEntityModel cityDetailEntityModel)
        {
            try
            {
                var cityDetail = mapper.Map<CityDetail>(cityDetailEntityModel);
                if (cityDetailEntityModel.Id == 0)
                {
                    dbContext.CityDetails.Add(cityDetail);
                }
                else
                {
                    
                        cityDetail = mapper.Map<CityDetail>(cityDetailEntityModel);
                        dbContext.CityDetails.Update(cityDetail);
                    
                }
                await dbContext.SaveChangesAsync();
                var result = mapper.Map<CityDetailEntityModel>(cityDetail);
                return cityDetailEntityModel;
            }
            catch (Exception ex)
            {

                throw new APIException(ErrorCodes.SomethingBadHappen, ex);
            }

        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            try
            {
                var city =await GetCityAsync(id);
                if (city != null)
                {
                   var cityDetail = mapper.Map<CityDetail>(city);
                   dbContext.CityDetails.Remove(cityDetail);
                   await dbContext.SaveChangesAsync();
                    return true;
                }
                return false; 
            }
            catch (Exception ex)
            {
                throw new APIException(ErrorCodes.SomethingBadHappen, ex);

            }
        }

        public async Task<DataResult<CityDetailEntityModel>> GetAllCityAsync(CityDetailSearch searchinfo)
        {
            try
            {
                var result = await dbContext.CityDetails.Where(s => s.start_date > searchinfo.StartDate && s.end_date < searchinfo.EndDate)
                       .ProjectTo<CityDetailEntityModel>(mapper.ConfigurationProvider).ToListAsync();
                if (searchinfo.Direction.ToLower().Contains("asc"))
                {
                    result = result.AsEnumerable().OrderBy(s => s.GetPropertyDynamic(searchinfo.ColumnName)).ToList();
                }
                else
                {
                    result = result.AsEnumerable().OrderByDescending(s => s.GetPropertyDynamic(searchinfo.ColumnName)).ToList();
                }

                var SortedResult = customPaging.GetPagingData(result.AsQueryable(), searchinfo.Page, searchinfo.PageSize, searchinfo.ColumnName, searchinfo.Direction);
                DataResult<CityDetailEntityModel> dataResult = new DataResult<CityDetailEntityModel>() { list = SortedResult.data.ToList(), totalCount = SortedResult.totalCount };
                return dataResult;
            }
            catch (Exception ex)
            {
                throw new APIException(ErrorCodes.SomethingBadHappen, ex);

            }

        }

        public async Task<CityDetailEntityModel> GetCityAsync(int id)
        {
            try
            {
                return await dbContext.CityDetails.Where(s => s.id == id).ProjectTo<CityDetailEntityModel>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new APIException(ErrorCodes.SomethingBadHappen, ex);

            }

        }
    }
}

    
