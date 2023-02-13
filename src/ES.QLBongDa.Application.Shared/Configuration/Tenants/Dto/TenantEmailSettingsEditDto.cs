using Abp.Auditing;
using ES.QLBongDa.Configuration.Dto;

namespace ES.QLBongDa.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}