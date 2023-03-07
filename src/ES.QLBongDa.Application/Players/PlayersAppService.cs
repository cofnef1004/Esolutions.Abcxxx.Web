using ES.QLBongDa.Clubs;
using ES.QLBongDa.Nations;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Players.Exporting;
using ES.QLBongDa.Players.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Players
{
    [AbpAuthorize(AppPermissions.Pages_Players)]
    public class PlayersAppService : QLBongDaAppServiceBase, IPlayersAppService
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IPlayersExcelExporter _playersExcelExporter;
        private readonly IRepository<Club, int> _lookup_clubRepository;
        private readonly IRepository<Nation, int> _lookup_nationRepository;

        public PlayersAppService(IRepository<Player> playerRepository, IPlayersExcelExporter playersExcelExporter, IRepository<Club, int> lookup_clubRepository, IRepository<Nation, int> lookup_nationRepository)
        {
            _playerRepository = playerRepository;
            _playersExcelExporter = playersExcelExporter;
            _lookup_clubRepository = lookup_clubRepository;
            _lookup_nationRepository = lookup_nationRepository;

        }

        public async Task<PagedResultDto<GetPlayerForViewDto>> GetAll(GetAllPlayersInput input)
        {

            var filteredPlayers = _playerRepository.GetAll()
                        .Include(e => e.ClubFk)
                        .Include(e => e.NationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Hoten.Contains(input.Filter) || e.Vitri.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HotenFilter), e => e.Hoten == input.HotenFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VitriFilter), e => e.Vitri == input.VitriFilter)
                        .WhereIf(input.MinsoaoFilter != null, e => e.soao >= input.MinsoaoFilter)
                        .WhereIf(input.MaxsoaoFilter != null, e => e.soao <= input.MaxsoaoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubMACLBFilter), e => e.ClubFk != null && e.ClubFk.MACLB == input.ClubMACLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NationmaqgFilter), e => e.NationFk != null && e.NationFk.maqg == input.NationmaqgFilter);

            var pagedAndFilteredPlayers = filteredPlayers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var players = from o in pagedAndFilteredPlayers
                          join o1 in _lookup_clubRepository.GetAll() on o.ClubId equals o1.Id into j1
                          from s1 in j1.DefaultIfEmpty()

                          join o2 in _lookup_nationRepository.GetAll() on o.NationId equals o2.Id into j2
                          from s2 in j2.DefaultIfEmpty()

                          select new
                          {

                              o.Hoten,
                              o.Vitri,
                              o.soao,
                              Id = o.Id,
                              ClubMACLB = s1 == null || s1.MACLB == null ? "" : s1.MACLB.ToString(),
                              Nationmaqg = s2 == null || s2.maqg == null ? "" : s2.maqg.ToString()
                          };

            var totalCount = await filteredPlayers.CountAsync();

            var dbList = await players.ToListAsync();
            var results = new List<GetPlayerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPlayerForViewDto()
                {
                    Player = new PlayerDto
                    {

                        Hoten = o.Hoten,
                        Vitri = o.Vitri,
                        soao = o.soao,
                        Id = o.Id,
                    },
                    ClubMACLB = o.ClubMACLB,
                    Nationmaqg = o.Nationmaqg
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPlayerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPlayerForViewDto> GetPlayerForView(int id)
        {
            var player = await _playerRepository.GetAsync(id);

            var output = new GetPlayerForViewDto { Player = ObjectMapper.Map<PlayerDto>(player) };

            if (output.Player.ClubId != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Player.ClubId);
                output.ClubMACLB = _lookupClub?.MACLB?.ToString();
            }

            if (output.Player.NationId != null)
            {
                var _lookupNation = await _lookup_nationRepository.FirstOrDefaultAsync((int)output.Player.NationId);
                output.Nationmaqg = _lookupNation?.maqg?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Players_Edit)]
        public async Task<GetPlayerForEditOutput> GetPlayerForEdit(EntityDto input)
        {
            var player = await _playerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPlayerForEditOutput { Player = ObjectMapper.Map<CreateOrEditPlayerDto>(player) };

            if (output.Player.ClubId != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Player.ClubId);
                output.ClubMACLB = _lookupClub?.MACLB?.ToString();
            }

            if (output.Player.NationId != null)
            {
                var _lookupNation = await _lookup_nationRepository.FirstOrDefaultAsync((int)output.Player.NationId);
                output.Nationmaqg = _lookupNation?.maqg?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPlayerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Players_Create)]
        protected virtual async Task Create(CreateOrEditPlayerDto input)
        {
            var player = ObjectMapper.Map<Player>(input);

            await _playerRepository.InsertAsync(player);

        }

        [AbpAuthorize(AppPermissions.Pages_Players_Edit)]
        protected virtual async Task Update(CreateOrEditPlayerDto input)
        {
            var player = await _playerRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, player);

        }

        [AbpAuthorize(AppPermissions.Pages_Players_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _playerRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetPlayersToExcel(GetAllPlayersForExcelInput input)
        {

            var filteredPlayers = _playerRepository.GetAll()
                        .Include(e => e.ClubFk)
                        .Include(e => e.NationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Hoten.Contains(input.Filter) || e.Vitri.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HotenFilter), e => e.Hoten == input.HotenFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VitriFilter), e => e.Vitri == input.VitriFilter)
                        .WhereIf(input.MinsoaoFilter != null, e => e.soao >= input.MinsoaoFilter)
                        .WhereIf(input.MaxsoaoFilter != null, e => e.soao <= input.MaxsoaoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClubMACLBFilter), e => e.ClubFk != null && e.ClubFk.MACLB == input.ClubMACLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NationmaqgFilter), e => e.NationFk != null && e.NationFk.maqg == input.NationmaqgFilter);

            var query = (from o in filteredPlayers
                         join o1 in _lookup_clubRepository.GetAll() on o.ClubId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_nationRepository.GetAll() on o.NationId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetPlayerForViewDto()
                         {
                             Player = new PlayerDto
                             {
                                 Hoten = o.Hoten,
                                 Vitri = o.Vitri,
                                 soao = o.soao,
                                 Id = o.Id
                             },
                             ClubMACLB = s1 == null || s1.MACLB == null ? "" : s1.MACLB.ToString(),
                             Nationmaqg = s2 == null || s2.maqg == null ? "" : s2.maqg.ToString()
                         });

            var playerListDtos = await query.ToListAsync();

            return _playersExcelExporter.ExportToFile(playerListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Players)]
        public async Task<List<PlayerClubLookupTableDto>> GetAllClubForTableDropdown()
        {
            return await _lookup_clubRepository.GetAll()
                .Select(club => new PlayerClubLookupTableDto
                {
                    Id = club.Id,
                    DisplayName = club == null || club.MACLB == null ? "" : club.MACLB.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Players)]
        public async Task<List<PlayerNationLookupTableDto>> GetAllNationForTableDropdown()
        {
            return await _lookup_nationRepository.GetAll()
                .Select(nation => new PlayerNationLookupTableDto
                {
                    Id = nation.Id,
                    DisplayName = nation == null || nation.maqg == null ? "" : nation.maqg.ToString()
                }).ToListAsync();
        }

    }
}