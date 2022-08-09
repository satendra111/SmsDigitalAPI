using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Services;
using DAL.Abstract;
using DAL.DbContexts;
using DAL.Entities;
using Domain.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class AccountRepository : IAccountRepository
    {
       
        private readonly UserEntityModel userEntityModel;
        public AccountRepository(IOptions<UserEntityModel> userEntityModel)
        {          
            this.userEntityModel = userEntityModel.Value;
        }
        public async Task<UserEntityModel> SignInAsync(string email, string password)
        {
            if(email==userEntityModel.Email && password==userEntityModel.Password)
            {
                return userEntityModel;
            }
            return null;

        }
        
       
       

    }
}
