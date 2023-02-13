using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa
{
    [DependsOn(typeof(QLBongDaXamarinSharedModule))]
    public class QLBongDaXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaXamarinIosModule).GetAssembly());
        }
    }
}