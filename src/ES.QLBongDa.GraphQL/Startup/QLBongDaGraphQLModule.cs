using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa.Startup
{
    [DependsOn(typeof(QLBongDaCoreModule))]
    public class QLBongDaGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}