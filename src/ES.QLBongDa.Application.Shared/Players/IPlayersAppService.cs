using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Players.Dtos;
using ES.QLBongDa.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace ES.QLBongDa.Players
{
    public interface IPlayersAppService : IApplicationService
    {
        Task<PagedResultDto<GetPlayerForViewDto>> GetAll(GetAllPlayersInput input);

        Task<GetPlayerForViewDto> GetPlayerForView(int id);

        Task<GetPlayerForEditOutput> GetPlayerForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditPlayerDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetPlayersToExcel(GetAllPlayersForExcelInput input);

        Task<List<PlayerClubLookupTableDto>> GetAllClubForTableDropdown();

        Task<List<PlayerNationLookupTableDto>> GetAllNationForTableDropdown();

    }
}