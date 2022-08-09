using Domain.Dto;
using Microsoft.IO;
using Newtonsoft.Json;

namespace API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseLoggingMiddleware> logger;
        private readonly Microsoft.IO.RecyclableMemoryStreamManager recyclableMemoryStreamManager;
        private IConfiguration config;


        public RequestResponseLoggingMiddleware(RequestDelegate paramsNext,
            ILogger<RequestResponseLoggingMiddleware> paramsLogger,
            IConfiguration configuration)
        {
            next = paramsNext;
            logger = paramsLogger;
            recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            config = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            string strReq = $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"X-Correlation-ID: {context.Request.Headers["x-correlation-id"]} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}";
            logger.LogInformation(strReq);
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string path = context.Request.Path;

            string strExclude = config.GetValue<string>("excludedLog");// "/swagger/index.html,/swagger/swagger-ui.css,/swagger/swagger-ui-standalone-preset.js,/swagger/swagger-ui-bundle.js";
            if (!string.IsNullOrWhiteSpace(strExclude))
            {
                string[] arrExclude = strExclude.Split(',');
                for (int i = 0; i < arrExclude.Length; i++)
                {
                    if (path.Equals(arrExclude[i].ToString()))
                    {
                        text = string.Empty;
                        break;
                    }
                }
            }
            string strRes = string.Empty;
            if (text.Length > 0)
            {
                var responseObject = JsonConvert.DeserializeObject<ApiResponseModel>(text);
                responseObject.RequestId = context.TraceIdentifier;
                text = JsonConvert.SerializeObject(responseObject);

                strRes = $"Http Response Information:{Environment.NewLine}" +
                                  $"Schema:{context.Request.Scheme} " +
                                  $"Host: {context.Request.Host} " +
                                  $"Path: {context.Request.Path} " +
                                  $"X-Correlation-ID: {context.TraceIdentifier} " +
                                  $"QueryString: {context.Request.QueryString} " +
                                  $"Response Body: {text}";
                logger.LogInformation(strRes);

            }

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}
