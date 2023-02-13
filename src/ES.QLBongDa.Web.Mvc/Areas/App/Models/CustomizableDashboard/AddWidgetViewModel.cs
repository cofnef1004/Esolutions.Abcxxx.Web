﻿using System.Collections.Generic;
using ES.QLBongDa.DashboardCustomization.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
