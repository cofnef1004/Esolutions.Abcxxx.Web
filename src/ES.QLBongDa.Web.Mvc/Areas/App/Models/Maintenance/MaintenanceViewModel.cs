using System.Collections.Generic;
using ES.QLBongDa.Caching.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}