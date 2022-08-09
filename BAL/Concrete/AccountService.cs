using BAL.Abstract;
using Core.Concrete;
using Core.Services;
using DAL.Abstract;
using Domain.CommonEntity;
using Domain.Dto;
using Domain.EntityModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository account;
      
       
        private readonly ITokenService tokenService;
       
        public AccountService(IAccountRepository account,            
               ITokenService tokenService
            )
        {
            this.account = account;            
            this.tokenService = tokenService;
           
        }
        public async Task<SignInResponse> SignInAsync(string email, string password)
        {

            var user = await account.SignInAsync(email, password);
            if (user == null) throw new APIException(ErrorCodes.UserDoesNotExists);
            
            var signInResponse = new SignInResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Token = tokenService.GenerateToken(user)
              
            };
            return signInResponse;
        }
       
        

       
    }
}
