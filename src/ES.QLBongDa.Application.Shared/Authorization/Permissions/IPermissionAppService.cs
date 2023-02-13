using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization.Permissions.Dto;

namespace ES.QLBongDa.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
