using Abp.AutoMapper;
using ES.QLBongDa.MultiTenancy;
using ES.QLBongDa.MultiTenancy.Dto;
using ES.QLBongDa.Web.Areas.App.Models.Common;

namespace ES.QLBongDa.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}