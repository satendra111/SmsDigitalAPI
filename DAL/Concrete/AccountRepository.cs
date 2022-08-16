using DAL.Abstract;
using Domain.EntityModel;
using Microsoft.Extensions.Options;

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
