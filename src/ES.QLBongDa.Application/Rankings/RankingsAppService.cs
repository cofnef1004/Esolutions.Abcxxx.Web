using ES.QLBongDa.Clubs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Rankings.Exporting;
using ES.QLBongDa.Rankings.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using ES.QLBongDa.Matchs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ES.QLBongDa.Rankings
{
    [AbpAuthorize(AppPermissions.Pages_Rankings)]
    public class RankingsAppService : QLBongDaAppServiceBase, IRankingsAppService
    {
        private readonly IRepository<Ranking> _rankingRepository;
        private readonly IMatchsAppService _matchRepository;
        private readonly IRepository<Club> _clubRepository;
        private readonly IRankingsExcelExporter _rankingsExcelExporter;
        private readonly IRepository<Club, int> _lookup_clubRepository;


        public RankingsAppService(IRepository<Ranking> rankingRepository, IRepository<Club> clubRepository, IMatchsAppService matchRepository, IRankingsExcelExporter rankingsExcelExporter, IRepository<Club, int> lookup_clubRepository)
        {
            _rankingRepository = rankingRepository;
            _matchRepository = matchRepository;
            _clubRepository = clubRepository;
            _rankingsExcelExporter = rankingsExcelExporter;
            _lookup_clubRepository = lookup_clubRepository;

        }

        public async Task<PagedResultDto<GetRankingForViewDto>> GetAll(GetAllRankingsInput input)
        {

            var filteredRankings = _rankingRepository.GetAll()
                        .Include(e => e.maclbFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinnamFilter != null, e => e.nam >= input.MinnamFilter)
                        .WhereIf(input.MaxnamFilter != null, e => e.nam <= input.MaxnamFilter)
                        .WhereIf(input.MinvongFilter != null, e => e.vong >= input.MinvongFilter)
                        .WhereIf(input.MaxvongFilter != null, e => e.vong <= input.MaxvongFilter)
                        .WhereIf(input.MintranFilter != null, e => e.tran >= input.MintranFilter)
                        .WhereIf(input.MaxtranFilter != null, e => e.tran <= input.MaxtranFilter)
                        .WhereIf(input.MinthangFilter != null, e => e.thang >= input.MinthangFilter)
                        .WhereIf(input.MaxthangFilter != null, e => e.thang <= input.MaxthangFilter)
                        .WhereIf(input.MinhoaFilter != null, e => e.hoa >= input.MinhoaFilter)
                        .WhereIf(input.MaxhoaFilter != null, e => e.hoa <= input.MaxhoaFilter)
                        .WhereIf(input.MinthuaFilter != null, e => e.thua >= input.MinthuaFilter)
                        .WhereIf(input.MaxthuaFilter != null, e => e.thua <= input.MaxthuaFilter)
                        .WhereIf(input.MinhieusoFilter != null, e => e.hieuso >= input.MinhieusoFilter)
                        .WhereIf(input.MaxhieusoFilter != null, e => e.hieuso <= input.MaxhieusoFilter)
                        .WhereIf(input.MindiemFilter != null, e => e.diem >= input.MindiemFilter)
                        .WhereIf(input.MaxdiemFilter != null, e => e.diem <= input.MaxdiemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubTENCLBFilter), e => e.maclbFk != null && e.maclbFk.TENCLB == input.ClubTENCLBFilter);

            var pagedAndFilteredRankings = filteredRankings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var rankings = from o in pagedAndFilteredRankings
                           join o1 in _lookup_clubRepository.GetAll() on o.maclb equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           select new
                           {

                               o.nam,
                               o.vong,
                               o.tran,
                               o.thang,
                               o.hoa,
                               o.thua,
                               o.hieuso,
                               o.diem,
                               Id = o.Id,
                               ClubTENCLB = s1 == null || s1.TENCLB == null ? "" : s1.TENCLB.ToString()
                           };

            var totalCount = await filteredRankings.CountAsync();

            var dbList = await rankings.ToListAsync();
            var results = new List<GetRankingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetRankingForViewDto()
                {
                    Ranking = new RankingDto
                    {

                        nam = o.nam,
                        vong = o.vong,
                        tran = o.tran,
                        thang = o.thang,
                        hoa = o.hoa,
                        thua = o.thua,
                        hieuso = o.hieuso,
                        diem = o.diem,
                        Id = o.Id,
                    },
                    ClubTENCLB = o.ClubTENCLB
                };

                results.Add(res);
            }

            return new PagedResultDto<GetRankingForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetRankingForViewDto> GetRankingForView(int id)
        {
            var ranking = await _rankingRepository.GetAsync(id);

            var output = new GetRankingForViewDto { Ranking = ObjectMapper.Map<RankingDto>(ranking) };

            if (output.Ranking.maclb != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Ranking.maclb);
                output.ClubTENCLB = _lookupClub?.TENCLB?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Rankings_Edit)]
        public async Task<GetRankingForEditOutput> GetRankingForEdit(EntityDto input)
        {
            var ranking = await _rankingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRankingForEditOutput { Ranking = ObjectMapper.Map<CreateOrEditRankingDto>(ranking) };

            if (output.Ranking.maclb != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Ranking.maclb);
                output.ClubTENCLB = _lookupClub?.TENCLB?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRankingDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Rankings_Create)]
        protected virtual async Task Create(CreateOrEditRankingDto input)
        {
            var ranking = ObjectMapper.Map<Ranking>(input);

            await _rankingRepository.InsertAsync(ranking);

        }

        [AbpAuthorize(AppPermissions.Pages_Rankings_Edit)]
        protected virtual async Task Update(CreateOrEditRankingDto input)
        {
            var ranking = await _rankingRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, ranking);

        }

        [AbpAuthorize(AppPermissions.Pages_Rankings_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _rankingRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetRankingsToExcel(GetAllRankingsForExcelInput input)
        {

            var filteredRankings = _rankingRepository.GetAll()
                        .Include(e => e.maclbFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinnamFilter != null, e => e.nam >= input.MinnamFilter)
                        .WhereIf(input.MaxnamFilter != null, e => e.nam <= input.MaxnamFilter)
                        .WhereIf(input.MinvongFilter != null, e => e.vong >= input.MinvongFilter)
                        .WhereIf(input.MaxvongFilter != null, e => e.vong <= input.MaxvongFilter)
                        .WhereIf(input.MintranFilter != null, e => e.tran >= input.MintranFilter)
                        .WhereIf(input.MaxtranFilter != null, e => e.tran <= input.MaxtranFilter)
                        .WhereIf(input.MinthangFilter != null, e => e.thang >= input.MinthangFilter)
                        .WhereIf(input.MaxthangFilter != null, e => e.thang <= input.MaxthangFilter)
                        .WhereIf(input.MinhoaFilter != null, e => e.hoa >= input.MinhoaFilter)
                        .WhereIf(input.MaxhoaFilter != null, e => e.hoa <= input.MaxhoaFilter)
                        .WhereIf(input.MinthuaFilter != null, e => e.thua >= input.MinthuaFilter)
                        .WhereIf(input.MaxthuaFilter != null, e => e.thua <= input.MaxthuaFilter)
                        .WhereIf(input.MinhieusoFilter != null, e => e.hieuso >= input.MinhieusoFilter)
                        .WhereIf(input.MaxhieusoFilter != null, e => e.hieuso <= input.MaxhieusoFilter)
                        .WhereIf(input.MindiemFilter != null, e => e.diem >= input.MindiemFilter)
                        .WhereIf(input.MaxdiemFilter != null, e => e.diem <= input.MaxdiemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubTENCLBFilter), e => e.maclbFk != null && e.maclbFk.TENCLB == input.ClubTENCLBFilter);

            var query = (from o in filteredRankings
                         join o1 in _lookup_clubRepository.GetAll() on o.maclb equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetRankingForViewDto()
                         {
                             Ranking = new RankingDto
                             {
                                 nam = o.nam,
                                 vong = o.vong,
                                 tran = o.tran,
                                 thang = o.thang,
                                 hoa = o.hoa,
                                 thua = o.thua,
                                 hieuso = o.hieuso,
                                 diem = o.diem,
                                 Id = o.Id
                             },
                             ClubTENCLB = s1 == null || s1.TENCLB == null ? "" : s1.TENCLB.ToString()
                         });

            var rankingListDtos = await query.ToListAsync();

            return _rankingsExcelExporter.ExportToFile(rankingListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Rankings)]
        public async Task<List<RankingClubLookupTableDto>> GetAllClubForTableDropdown()
        {
            return await _lookup_clubRepository.GetAll()
                .Select(club => new RankingClubLookupTableDto
                {
                    Id = club.Id,
                    DisplayName = club == null || club.TENCLB == null ? "" : club.TENCLB.ToString()
                }).ToListAsync();
        }

        public async Task<GetRankingForViewDto> ViewResult(int id)
        {
            var ranking = await _rankingRepository.GetAsync(id);
            var output = new GetRankingForViewDto { Ranking = ObjectMapper.Map<RankingDto>(ranking) };
            var getrs = await _matchRepository.GetMatchForView(id);
            var point = await GetRankingForView(id);
            int first = getrs.Match.Ketqua.IndexOf("-");
            int home = Convert.ToInt32(getrs.Match.Ketqua.Substring(0, first));
            int last = getrs.Match.Ketqua.LastIndexOf("-");
            int away = Convert.ToInt32(getrs.Match.Ketqua.Substring(last + 1));
            if (point.Ranking.vong < getrs.Match.Vong)
            {
                if (home > away)
                {
                    point.Ranking.diem += 3;
                    point.Ranking.tran += 1;
                    point.Ranking.thang += 1;
                }
                if (home == away)
                {
                    point.Ranking.diem += 1;
                    point.Ranking.tran += 1;
                    point.Ranking.hoa += 1;
                }
                if (home < away)
                {
                    point.Ranking.diem += 0;
                    point.Ranking.tran += 1;
                    point.Ranking.thua += 1;
                }
            }
            return (output);
        }

    }
}