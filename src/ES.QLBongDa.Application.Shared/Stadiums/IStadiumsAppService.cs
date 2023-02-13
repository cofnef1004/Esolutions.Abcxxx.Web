using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Stadiums.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Stadiums
{
    public interface IStadiumsAppService : IApplicationService
    {
        Task<PagedResultDto<GetStadiumForViewDto>> GetAll(GetAllStadiumsInput input);

        Task<GetStadiumForViewDto> GetStadiumForView(int id);

        Task<GetStadiumForEditOutput> GetStadiumForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditStadiumDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetStadiumsToExcel(GetAllStadiumsForExcelInput input);

    }
}