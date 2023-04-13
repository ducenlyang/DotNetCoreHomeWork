using DotNetCoreHomeWork.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DotNetCoreHomeWork.Api.Extension
{
    public static class MiddlewareExtension
    {
        /// <summary>
        /// customer globle Middleware
        /// </summary>
        /// <param name="app"></param>
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(CustomerExceptionMiddleware));
        }
    }
}
