using System.Collections.Generic;
using ES.QLBongDa.Authorization.Permissions.Dto;

namespace ES.QLBongDa.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}