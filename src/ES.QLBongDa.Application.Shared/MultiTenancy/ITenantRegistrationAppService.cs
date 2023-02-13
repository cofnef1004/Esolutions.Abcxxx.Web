using System.Threading.Tasks;
using Abp.Application.Services;
using ES.QLBongDa.Editions.Dto;
using ES.QLBongDa.MultiTenancy.Dto;

namespace ES.QLBongDa.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}