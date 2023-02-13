using Abp.AutoMapper;
using ES.QLBongDa.MultiTenancy.Dto;

namespace ES.QLBongDa.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}