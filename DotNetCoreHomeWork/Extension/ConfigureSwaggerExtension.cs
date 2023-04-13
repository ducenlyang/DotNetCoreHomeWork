using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace DotNetCoreHomeWork.Api.Extension
{
    public static class ConfigureSwagger
    {
        public static void ConfigureSwaggerUp(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Swagger Api Document",
                    Version = "v1",
                    Description = $"DotNetHomeWork.Api HTTP API V1",
                });
                c.OrderActionsBy(o => o.RelativePath);
            });
        }
    }
}
