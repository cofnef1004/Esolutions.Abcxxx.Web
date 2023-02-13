using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa
{
    public class QLBongDaCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaCoreSharedModule).GetAssembly());
        }
    }
}