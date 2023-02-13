using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ES.QLBongDa.Authorization;

namespace ES.QLBongDa
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(QLBongDaApplicationSharedModule),
        typeof(QLBongDaCoreModule)
        )]
    public class QLBongDaApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaApplicationModule).GetAssembly());
        }
    }
}