using Core.Concrete;
using Domain.Dto;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature?.Error != null)
                    {
                        ApiResponseModel response;

                        if (contextFeature.Error is APIException apiException)
                        {
                            if (((APIException)contextFeature.Error).Exception != null)
                            {
                              
                                logger.LogError(((APIException)contextFeature.Error).Exception, apiException.Error.ToString());
                            }
                            else
                            {
                                logger.LogError($"Error : ${contextFeature.Error.Message}");
                            }

                            response = new ApiResponseModel(apiException.Error);
                        }
                        else
                        {
                            logger.LogError($"Error : ${contextFeature.Error.Message}");

                            response = new ApiResponseModel(ErrorCodes.SomethingBadHappen);
                        }


                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web)
                        {
                            WriteIndented = true
                        };

                        await context.Response.WriteAsJsonAsync<ApiResponseModel>(
                            value: response,
                            options: jsonSerializerOptions,
                            contentType: "application/problem+json; charset=utf-8"
                            );
                    }
                });
            });
        }
    }
}
