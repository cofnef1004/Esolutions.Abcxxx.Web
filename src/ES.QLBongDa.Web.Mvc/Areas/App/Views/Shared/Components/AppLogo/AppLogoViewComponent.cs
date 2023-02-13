using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Layout;
using ES.QLBongDa.Web.Session;
using ES.QLBongDa.Web.Views;

namespace ES.QLBongDa.Web.Areas.App.Views.Shared.Components.AppLogo
{
    public class AppLogoViewComponent : QLBongDaViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppLogoViewComponent(
            IPerRequestSessionCache sessionCache
        )
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync(string logoSkin = null, string logoClass = "")
        {
            var headerModel = new LogoViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                LogoSkinOverride = logoSkin,
                LogoClassOverride = logoClass
            };

            return View(headerModel);
        }
    }
}
