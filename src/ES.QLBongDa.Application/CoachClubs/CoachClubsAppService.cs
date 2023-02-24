using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.CoachClubs.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.CoachClubs
{
    [AbpAuthorize(AppPermissions.Pages_CoachClubs)]
    public class CoachClubsAppService : QLBongDaAppServiceBase, ICoachClubsAppService
    {
        private readonly IRepository<CoachClub> _coachClubRepository;

        public CoachClubsAppService(IRepository<CoachClub> coachClubRepository)
        {
            _coachClubRepository = coachClubRepository;

        }

        public async Task<PagedResultDto<GetCoachClubForViewDto>> GetAll(GetAllCoachClubsInput input)
        {

            var filteredCoachClubs = _coachClubRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Mahlv.Contains(input.Filter) || e.MACLB.Contains(input.Filter) || e.Vaitro.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MahlvFilter), e => e.Mahlv == input.MahlvFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MACLBFilter), e => e.MACLB == input.MACLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VaitroFilter), e => e.Vaitro == input.VaitroFilter);

            var pagedAndFilteredCoachClubs = filteredCoachClubs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var coachClubs = from o in pagedAndFilteredCoachClubs
                             select new
                             {

                                 o.Mahlv,
                                 o.MACLB,
                                 o.Vaitro,
                                 Id = o.Id
                             };

            var totalCount = await filteredCoachClubs.CountAsync();

            var dbList = await coachClubs.ToListAsync();
            var results = new List<GetCoachClubForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCoachClubForViewDto()
                {
                    CoachClub = new CoachClubDto
                    {

                        Mahlv = o.Mahlv,
                        MACLB = o.MACLB,
                        Vaitro = o.Vaitro,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCoachClubForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_CoachClubs_Edit)]
        public async Task<GetCoachClubForEditOutput> GetCoachClubForEdit(EntityDto input)
        {
            var coachClub = await _coachClubRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCoachClubForEditOutput { CoachClub = ObjectMapper.Map<CreateOrEditCoachClubDto>(coachClub) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCoachClubDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_CoachClubs_Create)]
        protected virtual async Task Create(CreateOrEditCoachClubDto input)
        {
            var coachClub = ObjectMapper.Map<CoachClub>(input);

            await _coachClubRepository.InsertAsync(coachClub);

        }

        [AbpAuthorize(AppPermissions.Pages_CoachClubs_Edit)]
        protected virtual async Task Update(CreateOrEditCoachClubDto input)
        {
            var coachClub = await _coachClubRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, coachClub);

        }

        [AbpAuthorize(AppPermissions.Pages_CoachClubs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _coachClubRepository.DeleteAsync(input.Id);
        }

    }
}