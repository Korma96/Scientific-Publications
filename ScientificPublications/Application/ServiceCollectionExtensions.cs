using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace ScientificPublications.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterSingletons<T>(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(T));
            var allTypes = assembly.ExportedTypes;
            var interfaces = allTypes.Where(t => t.IsInterface && typeof(T).IsAssignableFrom(t) && t != typeof(T)).ToList();

            foreach (var i in interfaces)
            {
                var implementation = allTypes.FirstOrDefault(t => t.IsClass && i.IsAssignableFrom(t));
                if (implementation != null)
                {
                    services.AddSingleton(i, implementation);
                }
            }
        }
    }
}
