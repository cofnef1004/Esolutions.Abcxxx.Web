﻿using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa
{
    public class QLBongDaClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QLBongDaClientModule).GetAssembly());
        }
    }
}
