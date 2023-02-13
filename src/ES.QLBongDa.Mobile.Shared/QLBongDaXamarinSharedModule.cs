using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa
{
    [DependsOn(typeof(QLBongDaClientModule), typeof(AbpAutoMapperModule))]
    public class QLBongDaXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaXamarinSharedModule).GetAssembly());
        }
    }
}