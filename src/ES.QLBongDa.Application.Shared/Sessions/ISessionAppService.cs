using System.Threading.Tasks;
using Abp.Application.Services;
using ES.QLBongDa.Sessions.Dto;

namespace ES.QLBongDa.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
