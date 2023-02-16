using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Rankings.Dtos;
using ES.QLBongDa.Dto;
using System.Collections.Generic;

namespace ES.QLBongDa.Rankings
{
    public interface IRankingsAppService : IApplicationService
    {
        Task<PagedResultDto<GetRankingForViewDto>> GetAll(GetAllRankingsInput input);

        Task<GetRankingForViewDto> GetRankingForView(int id);

        Task<GetRankingForEditOutput> GetRankingForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditRankingDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetRankingsToExcel(GetAllRankingsForExcelInput input);

        Task<List<RankingClubLookupTableDto>> GetAllClubForTableDropdown();

    }
}