using System.Collections.Generic;
using ES.QLBongDa.Authorization.Permissions.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}