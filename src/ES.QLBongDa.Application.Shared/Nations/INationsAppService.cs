using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Nations.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Nations
{
    public interface INationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetNationForViewDto>> GetAll(GetAllNationsInput input);

        Task<GetNationForViewDto> GetNationForView(int id);

        Task<GetNationForEditOutput> GetNationForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditNationDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetNationsToExcel(GetAllNationsForExcelInput input);

    }
}