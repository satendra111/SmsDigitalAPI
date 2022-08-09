using Domain.Dto;
using Domain.EntityModel;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface IAccountService
    {
        Task<SignInResponse> SignInAsync(string email, string password);
       
        
    }
}
