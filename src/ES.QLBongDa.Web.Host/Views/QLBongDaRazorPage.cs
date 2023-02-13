using Abp.AspNetCore.Mvc.Views;

namespace ES.QLBongDa.Web.Views
{
    public abstract class QLBongDaRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected QLBongDaRazorPage()
        {
            LocalizationSourceName = QLBongDaConsts.LocalizationSourceName;
        }
    }
}
