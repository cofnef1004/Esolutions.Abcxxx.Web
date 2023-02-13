using System.Collections.Generic;
using ES.QLBongDa.Organizations.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.Common
{
    public interface IOrganizationUnitsEditViewModel
    {
        List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        List<string> MemberedOrganizationUnits { get; set; }
    }
}