using Abp.AspNetCore.Mvc.ViewComponents;

namespace ES.QLBongDa.Web.Views
{
    public abstract class QLBongDaViewComponent : AbpViewComponent
    {
        protected QLBongDaViewComponent()
        {
            LocalizationSourceName = QLBongDaConsts.LocalizationSourceName;
        }
    }
}