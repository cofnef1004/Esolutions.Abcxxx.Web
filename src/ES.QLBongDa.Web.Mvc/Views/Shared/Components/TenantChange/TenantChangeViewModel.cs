using Abp.AutoMapper;
using ES.QLBongDa.Sessions.Dto;

namespace ES.QLBongDa.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}