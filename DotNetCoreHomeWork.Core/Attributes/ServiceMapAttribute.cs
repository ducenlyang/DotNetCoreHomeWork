using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreHomeWork.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ServiceMapToAttribute : Attribute
    {
        /// <summary>
        /// ServiceLifetime
        /// </summary>
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;

        /// <summary>
        /// mMapping ServiceType
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Require a type to be specified through a constructor
        /// </summary>
        /// <param name="serviceType">serviceType</param>
        public ServiceMapToAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }
    }
}
