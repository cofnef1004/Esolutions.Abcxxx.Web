using Abp.Authorization;
using ES.QLBongDa.Authorization.Roles;
using ES.QLBongDa.Authorization.Users;

namespace ES.QLBongDa.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
