using DotNetCoreHomeWork.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DotNetCoreHomeWork.Api.Extension
{
    public static class AppExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.Contains("DotNetCoreHomeWork.Core"));
            if (assembly == null) return;
            foreach (var type in assembly.GetTypes())
            {
                var serviceAttribute = type.GetCustomAttribute<ServiceMapToAttribute>();

                if (serviceAttribute != null)
                {
                    switch (serviceAttribute.Lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(serviceAttribute.ServiceType, type);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(serviceAttribute.ServiceType, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(serviceAttribute.ServiceType, type);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
