using Core.Concrete;


namespace Domain.Dto
{

    public class ApiResponseModel
    {
        [Newtonsoft.Json.JsonConstructor]
        public ApiResponseModel(ApiResponseInfoModel error, object data)
        {
            Data = data;
            Error = error;
        }
        public ApiResponseModel(ErrorCodes code)
        {
            Error = new ApiResponseInfoModel(code);

        }
        public ApiResponseModel(object data)
        {
            Data = data;

        }

        public ApiResponseInfoModel Error { get; }
        public string RequestId { get; set; }
        public object Data { get; }
        public class ApiResponseInfoModel
        {
            [Newtonsoft.Json.JsonConstructor]
            public ApiResponseInfoModel(int code, string message)
            {
                Code = code;
                Message = message;
            }
            public ApiResponseInfoModel(ErrorCodes code)
            {
                Code = (int)code;
                Message = code.ToString();
            }
            public int Code { get; }
            public string Message { get; }
        }
    }
}