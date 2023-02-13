using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa
{
    [DependsOn(typeof(QLBongDaCoreSharedModule))]
    public class QLBongDaApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaApplicationSharedModule).GetAssembly());
        }
    }
}