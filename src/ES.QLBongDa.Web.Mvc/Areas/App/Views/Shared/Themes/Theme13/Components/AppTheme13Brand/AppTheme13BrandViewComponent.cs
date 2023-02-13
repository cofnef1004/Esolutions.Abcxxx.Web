using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Layout;
using ES.QLBongDa.Web.Session;
using ES.QLBongDa.Web.Views;

namespace ES.QLBongDa.Web.Areas.App.Views.Shared.Themes.Theme13.Components.AppTheme13Brand
{
    public class AppTheme13BrandViewComponent : QLBongDaViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme13BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
