using Abp.AutoMapper;
using ES.QLBongDa.MultiTenancy.Dto;

namespace ES.QLBongDa.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
