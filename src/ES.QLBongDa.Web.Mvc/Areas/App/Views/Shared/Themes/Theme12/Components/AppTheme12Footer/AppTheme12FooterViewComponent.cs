﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Layout;
using ES.QLBongDa.Web.Session;
using ES.QLBongDa.Web.Views;

namespace ES.QLBongDa.Web.Areas.App.Views.Shared.Themes.Theme12.Components.AppTheme12Footer
{
    public class AppTheme12FooterViewComponent : QLBongDaViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme12FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
