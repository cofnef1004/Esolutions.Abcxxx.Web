using System.Threading.Tasks;
using Abp.Authorization.Users;
using ES.QLBongDa.Authorization.Users;

namespace ES.QLBongDa.Authorization
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetAdminAsync(this UserManager userManager)
        {
            return await userManager.FindByNameAsync(AbpUserBase.AdminUserName);
        }
    }
}
