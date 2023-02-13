using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using ES.QLBongDa.Configuration;

namespace ES.QLBongDa.Test.Base.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(QLBongDaTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
