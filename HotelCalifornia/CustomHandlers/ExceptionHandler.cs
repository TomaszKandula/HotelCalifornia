using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using HotelCalifornia.Backend.Core.Models;
using HotelCalifornia.Backend.Shared.Cors;
using HotelCalifornia.Backend.Core.Exceptions;
using HotelCalifornia.Backend.Shared.Resources;
using Newtonsoft.Json;

namespace HotelCalifornia.CustomHandlers
{
    public static class ExceptionHandler
    {
        public static void Handle(IApplicationBuilder AApplication)
        {
            AApplication.Run(async AHttpContext => 
            {
                var LExceptionHandlerPathFeature = AHttpContext.Features.Get<IExceptionHandlerPathFeature>();
                var LErrorException = LExceptionHandlerPathFeature.Error;
                AHttpContext.Response.ContentType = "application/json";

                string LResult;
                switch (LErrorException)
                {

                    case ValidationException LException:
                    {
                        var LAppError = new ApplicationError(LException.ErrorCode, LException.Message, LException.ValidationResult);
                        LResult = JsonConvert.SerializeObject(LAppError);
                        AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                    case BusinessException LException:
                    {
                        var LAppError = new ApplicationError(LException.ErrorCode, LException.Message);
                        LResult = JsonConvert.SerializeObject(LAppError);
                        AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                    default:
                    {
                        var LAppError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
                        LResult = JsonConvert.SerializeObject(LAppError);
                        AHttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    }

                }

                CorsHeaders.Ensure(AHttpContext);
                await AHttpContext.Response.WriteAsync(LResult);
            });
        }
    }
}