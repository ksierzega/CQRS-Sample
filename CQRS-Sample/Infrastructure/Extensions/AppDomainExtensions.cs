using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CQRS_Sample.Infrastructure.Extensions
{
    public static class AppDomainExtensions
    {
        public static Assembly[] GetAppAssemblies(this AppDomain @this)
        {
            return @this.GetAssemblies()
                .Where(x => x.FullName.StartsWith("CQRS-Sample"))
                .ToArray();
        }

        public static IEnumerable<Type> GetAppTypes(this AppDomain @this)
        {
            return @this.GetAssemblies()
                .Where(x => x.FullName.StartsWith("CQRS-Sample"))
                .SelectMany(x => x.GetTypes());
        }
    }
}