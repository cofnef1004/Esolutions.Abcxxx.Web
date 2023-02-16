using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Managers.Dtos;
using ES.QLBongDa.Dto;
using System.Collections.Generic;

namespace ES.QLBongDa.Managers
{
    public interface IManagersAppService : IApplicationService
    {
        Task<PagedResultDto<GetManagerForViewDto>> GetAll(GetAllManagersInput input);

        Task<GetManagerForViewDto> GetManagerForView(int id);

        Task<GetManagerForEditOutput> GetManagerForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditManagerDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetManagersToExcel(GetAllManagersForExcelInput input);

        Task<List<ManagerNationLookupTableDto>> GetAllNationForTableDropdown();

    }
}