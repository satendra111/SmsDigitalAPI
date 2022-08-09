using BAL.Abstract;
using BAL.Concrete;
using DAL.Abstract;
using DAL.Automapper;
using DAL.Concrete;
using DAL.DbContexts;
using Domain.EntityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SelfOnBoarding.Bll.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Helper
{
    public static class DependencyResolver
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ApiDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("dbConnectionString")));

            
            services.AddAutoMapper(typeof(AutoMapperProfileDal));

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountService, AccountService>();


            
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddTransient<ICustomPaging<CityDetailEntityModel>, CustomPaging<CityDetailEntityModel>>();
            services.AddTransient<ICityService, CityService>();

            
        }
    }
}
