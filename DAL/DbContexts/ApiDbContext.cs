using Azure.Identity;
using DAL.Entities;
using Domain.CommonEntity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContexts
{
    public class ApiDbContext : DbContext
    {
        private readonly IOptions<ConnectionStrings> connectionStrings;
        public ApiDbContext(IOptions<ConnectionStrings> connectionStrings,
            DbContextOptions<ApiDbContext> options) : base(options)
        {
            this.connectionStrings = connectionStrings;

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CityDetail>().HasData(
                new CityDetail { id = 1, city = "Neftegorsk", start_date = DateTime.Now.Date, end_date = DateTime.Now.Date.AddDays(10), price = 55.82, status = "Seldom", color = "#fd4e19" }
                
            );
        }
        internal DbSet<CityDetail> CityDetails { get; set; }
      

    }

    
}
