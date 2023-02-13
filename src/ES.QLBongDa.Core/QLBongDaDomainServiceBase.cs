using Abp.Domain.Services;

namespace ES.QLBongDa
{
    public abstract class QLBongDaDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected QLBongDaDomainServiceBase()
        {
            LocalizationSourceName = QLBongDaConsts.LocalizationSourceName;
        }
    }
}
