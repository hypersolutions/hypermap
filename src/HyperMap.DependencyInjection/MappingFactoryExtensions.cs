using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace HyperMap.DependencyInjection
{
    public static class MappingFactoryExtensions
    {
        public static IMappingFactory RegisterWith(this IMappingFactory factory, IServiceCollection services)
        {
            foreach (var mappingInstance in factory.GetAll())
            {
                var mappingInterfaceType = mappingInstance.GetType().GetInterfaces().First();
                services.AddSingleton(mappingInterfaceType, mappingInstance);
            }
            
            return factory;
        }
    }
}
