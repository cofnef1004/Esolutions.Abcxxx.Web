using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Clubs.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Clubs
{
    public interface IClubsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClubForViewDto>> GetAll(GetAllClubsInput input);

        Task<GetClubForViewDto> GetClubForView(int id);

        Task<GetClubForEditOutput> GetClubForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditClubDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetClubsToExcel(GetAllClubsForExcelInput input);

        Task<PagedResultDto<ClubStadiumLookupTableDto>> GetAllStadiumForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<ClubVilageLookupTableDto>> GetAllVilageForLookupTable(GetAllForLookupTableInput input);

    }
}