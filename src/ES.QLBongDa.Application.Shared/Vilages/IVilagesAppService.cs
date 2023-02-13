using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Vilages.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Vilages
{
    public interface IVilagesAppService : IApplicationService
    {
        Task<PagedResultDto<GetVilageForViewDto>> GetAll(GetAllVilagesInput input);

        Task<GetVilageForViewDto> GetVilageForView(int id);

        Task<GetVilageForEditOutput> GetVilageForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditVilageDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetVilagesToExcel(GetAllVilagesForExcelInput input);

    }
}