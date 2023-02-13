using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ES.QLBongDa.Configure;
using ES.QLBongDa.Startup;
using ES.QLBongDa.Test.Base;

namespace ES.QLBongDa.GraphQL.Tests
{
    [DependsOn(
        typeof(QLBongDaGraphQLModule),
        typeof(QLBongDaTestBaseModule))]
    public class QLBongDaGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaGraphQLTestModule).GetAssembly());
        }
    }
}