using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.CoachClubs.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.CoachClubs
{
    public interface ICoachClubsAppService : IApplicationService
    {
        Task<PagedResultDto<GetCoachClubForViewDto>> GetAll(GetAllCoachClubsInput input);

        Task<GetCoachClubForEditOutput> GetCoachClubForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCoachClubDto input);

        Task Delete(EntityDto input);

    }
}