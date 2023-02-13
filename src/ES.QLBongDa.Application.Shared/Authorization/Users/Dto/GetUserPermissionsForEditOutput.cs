using System.Collections.Generic;
using ES.QLBongDa.Authorization.Permissions.Dto;

namespace ES.QLBongDa.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}