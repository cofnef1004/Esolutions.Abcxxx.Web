using System.Threading.Tasks;
using Abp.Configuration;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Configuration;
using ES.QLBongDa.Web.Areas.App.Models.Layout;
using ES.QLBongDa.Web.Session;
using ES.QLBongDa.Web.Views;

namespace ES.QLBongDa.Web.Areas.App.Views.Shared.Components.AppSubscriptionBar
{
    public class AppSubscriptionBarViewComponent : QLBongDaViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppSubscriptionBarViewComponent(
            IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync(string cssClass = "btn btn-icon btn-active-light-primary position-relative w-30px h-30px w-md-40px h-md-40px me-2")
        {
            var model = new SubscriptionBarViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                SubscriptionExpireNotifyDayCount = SettingManager.GetSettingValue<int>(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount),
                CssClass = cssClass
            };

            return View(model);
        }

    }
}
