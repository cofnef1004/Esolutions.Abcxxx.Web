using Abp.AspNetCore.Mvc.ViewComponents;

namespace ES.QLBongDa.Web.Public.Views
{
    public abstract class QLBongDaViewComponent : AbpViewComponent
    {
        protected QLBongDaViewComponent()
        {
            LocalizationSourceName = QLBongDaConsts.LocalizationSourceName;
        }
    }
}