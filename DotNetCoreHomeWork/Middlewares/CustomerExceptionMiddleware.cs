using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Net;

namespace DotNetCoreHomeWork.Api.Middlewares
{
    public class CustomerExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomerExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json;charset=utf-8";

                var result = new 
                {
                    Code = HttpStatusCode.InternalServerError,
                    Msg = "Internal Server Error",
                    ErrorInfo = ex.Message
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings()
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                }));
            }
        }
    }
}
