using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace ES.QLBongDa.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
