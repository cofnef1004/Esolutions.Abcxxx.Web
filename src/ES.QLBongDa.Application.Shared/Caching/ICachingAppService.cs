using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Caching.Dto;

namespace ES.QLBongDa.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultDto<CacheDto> GetAllCaches();

        Task ClearCache(EntityDto<string> input);

        Task ClearAllCaches();
    }
}
