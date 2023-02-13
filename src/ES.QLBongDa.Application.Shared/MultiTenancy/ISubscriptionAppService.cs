using System.Threading.Tasks;
using Abp.Application.Services;

namespace ES.QLBongDa.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
