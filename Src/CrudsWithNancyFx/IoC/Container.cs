using StructureMap;
using StructureMap.Graph;

namespace CrudsWithNancyFx.IoC
{
    public class Container
    {
        public static void Configure(IContainer container)
        {
            container.Configure(config => config.Scan(c =>
            {
                c.TheCallingAssembly();
                c.WithDefaultConventions();
            }));
        }
    }
}