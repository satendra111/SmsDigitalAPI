
using Domain.EntityModel;
using System.IdentityModel.Tokens.Jwt;

namespace BAL.Abstract
{
    public interface ITokenService
    {
        string GenerateToken(UserEntityModel userEntityModel);
      
    }
}
