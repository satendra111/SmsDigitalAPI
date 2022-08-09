using Domain.CommonEntity;
using Domain.Dto;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate next;
        private readonly CorrelationIdOptions options;
        public CorrelationIdMiddleware(RequestDelegate paramsNext, IOptions<CorrelationIdOptions> paramsOptions)
        {
            if (paramsNext == null)
            {
                throw new ArgumentNullException(nameof(paramsNext));
            }

            next = paramsNext ?? throw new ArgumentNullException(nameof(paramsNext));

            options = paramsOptions.Value;
        }
        public async Task Invoke(HttpContext context)
        {           

            var originBody = context.Response.Body;
            try
            {
                var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await next(context).ConfigureAwait(false);

                memStream.Position = 0;
                var responseBody = new StreamReader(memStream).ReadToEnd();

                //Custom logic to modify response
                var responseObject = JsonConvert.DeserializeObject<ApiResponseModel>(responseBody);
                responseObject.RequestId = context.TraceIdentifier;
                responseBody = JsonConvert.SerializeObject(responseObject);

                var memoryStreamModified = new MemoryStream();
                var sw = new StreamWriter(memoryStreamModified);
                sw.Write(responseBody);
                sw.Flush();
                memoryStreamModified.Position = 0;

                await memoryStreamModified.CopyToAsync(originBody).ConfigureAwait(false);
            }
            finally
            {
                context.Response.Body = originBody;
            }
        }
    }
}
