using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public enum ErrorCodes
    {
        Success = 200,
        ServerError=503,
        InvalidRequest = 403,      
        UserDoesNotExists = 5003,
        SomethingBadHappen = 5004,
        InValidToken = 5006,
        InValidTokenAndUserId = 5007,      
    }
  
}
