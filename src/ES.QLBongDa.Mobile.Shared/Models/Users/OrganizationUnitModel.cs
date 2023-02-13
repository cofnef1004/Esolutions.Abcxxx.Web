using Abp.AutoMapper;
using ES.QLBongDa.Organizations.Dto;

namespace ES.QLBongDa.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}