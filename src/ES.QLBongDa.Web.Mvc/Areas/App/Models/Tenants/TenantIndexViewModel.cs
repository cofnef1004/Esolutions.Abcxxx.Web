using System.Collections.Generic;
using ES.QLBongDa.Editions.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}