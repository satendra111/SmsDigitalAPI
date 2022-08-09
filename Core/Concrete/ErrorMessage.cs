using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public static class ErrorMessageFromResource
    {

        public static string GetMessage(string key)
        {

            ResourceManager rm = new ResourceManager(@"en-us.resx",
                                    System.Reflection.Assembly.Load("Resources"));
            String strName = rm.GetString(key);
            return strName;
        }
    }
    public static class ErrorMessage
    {
        public const string EmailIdDuplicate = "Account already exists with this email address";
        public const string EmailIdNotExists = "No account exist with this email address";
        public const string InvalidRequestMessage = "Invalid Request";
        public const string SuccessMessage = "Success";
        public const string ErrorMessages = "Error";
        public const string FailedToComputeHash = "FailedToComputeHash";
        public const string ParameterNullOrEmpty = "ParameterNullOrEmpty";
        public const string FailedToVerifyHash = "FailedToVerifyHash";

    }
    public class Status
    {
        public const string Pending = "P";
        public const string Active = "A";
        public const string Rejected = "R";
    }
    public class TabName
    {
        public const int Business = 1;
        public const int Documentation = 2;
        public const int BankDetails = 3;
        public const int IntegrationDetails = 4;
        public const int CommunicationDetails = 5;
    }
    public class Environments
    {
        public const string Development = "Development";
        public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    }
}
