using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Matchs.Dtos;
using ES.QLBongDa.Dto;
using System.Collections.Generic;


namespace ES.QLBongDa.Matchs
{
    public interface IMatchsAppService : IApplicationService
    {
        Task<PagedResultDto<GetMatchForViewDto>> GetAll(GetAllMatchsInput input);

        Task<GetMatchForViewDto> GetMatchForView(int id);

        Task<GetMatchForEditOutput> GetMatchForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditMatchDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetMatchsToExcel(GetAllMatchsForExcelInput input);

        Task<List<MatchClubLookupTableDto>> GetAllClubForTableDropdown();

        Task<List<MatchStadiumLookupTableDto>> GetAllStadiumForTableDropdown();
     

    }
}