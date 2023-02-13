using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace ES.QLBongDa.Web.Public.Views
{
    public abstract class QLBongDaRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected QLBongDaRazorPage()
        {
            LocalizationSourceName = QLBongDaConsts.LocalizationSourceName;
        }
    }
}
