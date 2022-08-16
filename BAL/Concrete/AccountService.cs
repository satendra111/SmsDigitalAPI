using BAL.Abstract;
using Core.Concrete;
using DAL.Abstract;
using Domain.Dto;

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
        /// <summary>
        /// SignIn method 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Task<SignInResponse></returns>

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
