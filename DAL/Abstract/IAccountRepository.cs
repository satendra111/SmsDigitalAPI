using Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IAccountRepository
    {
        Task<UserEntityModel> SignInAsync(string email, string password);
       
    }
}
