using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa
{
    [DependsOn(typeof(QLBongDaXamarinSharedModule))]
    public class QLBongDaXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaXamarinAndroidModule).GetAssembly());
        }
    }
}