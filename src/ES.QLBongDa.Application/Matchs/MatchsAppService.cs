using ES.QLBongDa.Clubs;
using ES.QLBongDa.Stadiums;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Matchs.Exporting;
using ES.QLBongDa.Matchs.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using ES.QLBongDa.Rankings;

namespace ES.QLBongDa.Matchs
{
    [AbpAuthorize(AppPermissions.Pages_Matchs)]
    public class MatchsAppService : QLBongDaAppServiceBase, IMatchsAppService
    {
        private readonly IRepository<Match> _matchRepository;
        private readonly IMatchsExcelExporter _matchsExcelExporter;
        private readonly IRepository<Club, int> _lookup_clubRepository;
        private readonly IRepository<Stadium, int> _lookup_stadiumRepository;

        public MatchsAppService(IRepository<Match> matchRepository, IMatchsExcelExporter matchsExcelExporter, IRepository<Club, int> lookup_clubRepository, IRepository<Stadium, int> lookup_stadiumRepository)
        {
            _matchRepository = matchRepository;
            _matchsExcelExporter = matchsExcelExporter;
            _lookup_clubRepository = lookup_clubRepository;
            _lookup_stadiumRepository = lookup_stadiumRepository;

        }

        public async Task<PagedResultDto<GetMatchForViewDto>> GetAll(GetAllMatchsInput input)
        {

            var filteredMatchs = _matchRepository.GetAll()
                        .Include(e => e.Maclb1Fk)
                        .Include(e => e.Maclb2Fk)
                        .Include(e => e.MasanFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Ketqua.Contains(input.Filter))
                        .WhereIf(input.MinNamFilter != null, e => e.Nam >= input.MinNamFilter)
                        .WhereIf(input.MaxNamFilter != null, e => e.Nam <= input.MaxNamFilter)
                        .WhereIf(input.MinVongFilter != null, e => e.Vong >= input.MinVongFilter)
                        .WhereIf(input.MaxVongFilter != null, e => e.Vong <= input.MaxVongFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KetquaFilter), e => e.Ketqua == input.KetquaFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubTENCLBFilter), e => e.Maclb1Fk != null && e.Maclb1Fk.TENCLB == input.ClubTENCLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubTENCLB2Filter), e => e.Maclb2Fk != null && e.Maclb2Fk.TENCLB == input.ClubTENCLB2Filter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StadiumTensanFilter), e => e.MasanFk != null && e.MasanFk.Tensan == input.StadiumTensanFilter);

            var pagedAndFilteredMatchs = filteredMatchs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var matchs = from o in pagedAndFilteredMatchs
                         join o1 in _lookup_clubRepository.GetAll() on o.Maclb1 equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_clubRepository.GetAll() on o.Maclb2 equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_stadiumRepository.GetAll() on o.Masan equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         select new
                         {

                             o.Nam,
                             o.Vong,
                             o.Ketqua,
                             Id = o.Id,
                             ClubTENCLB = s1 == null || s1.TENCLB == null ? "" : s1.TENCLB.ToString(),
                             ClubTENCLB2 = s2 == null || s2.TENCLB == null ? "" : s2.TENCLB.ToString(),
                             StadiumTensan = s3 == null || s3.Tensan == null ? "" : s3.Tensan.ToString()
                         };

            var totalCount = await filteredMatchs.CountAsync();

            var dbList = await matchs.ToListAsync();
            var results = new List<GetMatchForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetMatchForViewDto()
                {
                    Match = new MatchDto
                    {

                        Nam = o.Nam,
                        Vong = o.Vong,
                        Ketqua = o.Ketqua,
                        Id = o.Id,
                    },
                    ClubTENCLB = o.ClubTENCLB,
                    ClubTENCLB2 = o.ClubTENCLB2,
                    StadiumTensan = o.StadiumTensan
                };

                results.Add(res);
            }

            return new PagedResultDto<GetMatchForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetMatchForViewDto> GetMatchForView(int id)
        {
            var match = await _matchRepository.GetAsync(id);

            var output = new GetMatchForViewDto { Match = ObjectMapper.Map<MatchDto>(match) };

            if (output.Match.Maclb1 != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Match.Maclb1);
                output.ClubTENCLB = _lookupClub?.TENCLB?.ToString();
            }

            if (output.Match.Maclb2 != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Match.Maclb2);
                output.ClubTENCLB2 = _lookupClub?.TENCLB?.ToString();
            }

            if (output.Match.Masan != null)
            {
                var _lookupStadium = await _lookup_stadiumRepository.FirstOrDefaultAsync((int)output.Match.Masan);
                output.StadiumTensan = _lookupStadium?.Tensan?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Matchs_Edit)]
        public async Task<GetMatchForEditOutput> GetMatchForEdit(EntityDto input)
        {
            var match = await _matchRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMatchForEditOutput { Match = ObjectMapper.Map<CreateOrEditMatchDto>(match) };

            if (output.Match.Maclb1 != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Match.Maclb1);
                output.ClubTENCLB = _lookupClub?.TENCLB?.ToString();
            }

            if (output.Match.Maclb2 != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Match.Maclb2);
                output.ClubTENCLB2 = _lookupClub?.TENCLB?.ToString();
            }

            if (output.Match.Masan != null)
            {
                var _lookupStadium = await _lookup_stadiumRepository.FirstOrDefaultAsync((int)output.Match.Masan);
                output.StadiumTensan = _lookupStadium?.Tensan?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditMatchDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Matchs_Create)]
        protected virtual async Task Create(CreateOrEditMatchDto input)
        {
            var match = ObjectMapper.Map<Match>(input);

            await _matchRepository.InsertAsync(match);

        }

        [AbpAuthorize(AppPermissions.Pages_Matchs_Edit)]
        protected virtual async Task Update(CreateOrEditMatchDto input)
        {
            var match = await _matchRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, match);

        }

        [AbpAuthorize(AppPermissions.Pages_Matchs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _matchRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetMatchsToExcel(GetAllMatchsForExcelInput input)
        {

            var filteredMatchs = _matchRepository.GetAll()
                        .Include(e => e.Maclb1Fk)
                        .Include(e => e.Maclb2Fk)
                        .Include(e => e.MasanFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Ketqua.Contains(input.Filter))
                        .WhereIf(input.MinNamFilter != null, e => e.Nam >= input.MinNamFilter)
                        .WhereIf(input.MaxNamFilter != null, e => e.Nam <= input.MaxNamFilter)
                        .WhereIf(input.MinVongFilter != null, e => e.Vong >= input.MinVongFilter)
                        .WhereIf(input.MaxVongFilter != null, e => e.Vong <= input.MaxVongFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KetquaFilter), e => e.Ketqua == input.KetquaFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubTENCLBFilter), e => e.Maclb1Fk != null && e.Maclb1Fk.TENCLB == input.ClubTENCLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubTENCLB2Filter), e => e.Maclb2Fk != null && e.Maclb2Fk.TENCLB == input.ClubTENCLB2Filter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StadiumTensanFilter), e => e.MasanFk != null && e.MasanFk.Tensan == input.StadiumTensanFilter);

            var query = (from o in filteredMatchs
                         join o1 in _lookup_clubRepository.GetAll() on o.Maclb1 equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_clubRepository.GetAll() on o.Maclb2 equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_stadiumRepository.GetAll() on o.Masan equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         select new GetMatchForViewDto()
                         {
                             Match = new MatchDto
                             {
                                 Nam = o.Nam,
                                 Vong = o.Vong,
                                 Ketqua = o.Ketqua,
                                 Id = o.Id
                             },
                             ClubTENCLB = s1 == null || s1.TENCLB == null ? "" : s1.TENCLB.ToString(),
                             ClubTENCLB2 = s2 == null || s2.TENCLB == null ? "" : s2.TENCLB.ToString(),
                             StadiumTensan = s3 == null || s3.Tensan == null ? "" : s3.Tensan.ToString()
                         });

            var matchListDtos = await query.ToListAsync();

            return _matchsExcelExporter.ExportToFile(matchListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Matchs)]
        public async Task<List<MatchClubLookupTableDto>> GetAllClubForTableDropdown()
        {
            return await _lookup_clubRepository.GetAll()
                .Select(club => new MatchClubLookupTableDto
                {
                    Id = club.Id,
                    DisplayName = club == null || club.TENCLB == null ? "" : club.TENCLB.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Matchs)]
        public async Task<List<MatchStadiumLookupTableDto>> GetAllStadiumForTableDropdown()
        {
            return await _lookup_stadiumRepository.GetAll()
                .Select(stadium => new MatchStadiumLookupTableDto
                {
                    Id = stadium.Id,
                    DisplayName = stadium == null || stadium.Tensan == null ? "" : stadium.Tensan.ToString()
                }).ToListAsync();
        }

    }
}