using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using ES.QLBongDa.Authorization.Users;
using ES.QLBongDa.MultiTenancy;

namespace ES.QLBongDa.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}