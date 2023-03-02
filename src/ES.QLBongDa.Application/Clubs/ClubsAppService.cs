using ES.QLBongDa.Stadiums;
using ES.QLBongDa.Vilages;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Clubs.Exporting;
using ES.QLBongDa.Clubs.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;
using ES.QLBongDa.ListHLVs;
using ES.QLBongDa.ListHLVs.Dtos;
using ES.QLBongDa.Managers;
using ES.QLBongDa.Managers.Dtos;

namespace ES.QLBongDa.Clubs
{
    [AbpAuthorize(AppPermissions.Pages_Clubs)]
    public class ClubsAppService : QLBongDaAppServiceBase, IClubsAppService
    {
        private readonly IRepository<ListHLV> _listHLVRepository;
        private readonly IRepository<Club> _clubRepository;
        private readonly IRepository<Manager> _managerRepository;
        private readonly IClubsExcelExporter _clubsExcelExporter;
        private readonly IRepository<Stadium, int> _lookup_stadiumRepository;
        private readonly IRepository<Vilage, int> _lookup_vilageRepository;

        public ClubsAppService(IRepository<Club> clubRepository, IRepository<Manager> managerRepository, IRepository<ListHLV> listHLVRepository, IClubsExcelExporter clubsExcelExporter, IRepository<Stadium, int> lookup_stadiumRepository, IRepository<Vilage, int> lookup_vilageRepository)
        {
            _clubRepository = clubRepository;
            _clubsExcelExporter = clubsExcelExporter;
            _lookup_stadiumRepository = lookup_stadiumRepository;
            _lookup_vilageRepository = lookup_vilageRepository;
            _listHLVRepository = listHLVRepository;
            _managerRepository = managerRepository;

        }

        public async Task<PagedResultDto<GetClubForViewDto>> GetAll(GetAllClubsInput input)
        {

            var filteredClubs = _clubRepository.GetAll()
                        .Include(e => e.StadiumFk)
                        .Include(e => e.VilageFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MACLB.Contains(input.Filter) || e.TENCLB.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MACLBFilter), e => e.MACLB == input.MACLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TENCLBFilter), e => e.TENCLB == input.TENCLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StadiumTensanFilter), e => e.StadiumFk != null && e.StadiumFk.Tensan == input.StadiumTensanFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VilagetentinhFilter), e => e.VilageFk != null && e.VilageFk.tentinh == input.VilagetentinhFilter);

            var pagedAndFilteredClubs = filteredClubs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clubs = from o in pagedAndFilteredClubs
                        join o1 in _lookup_stadiumRepository.GetAll() on o.StadiumId equals o1.Id into j1
                        from s1 in j1.DefaultIfEmpty()

                        join o2 in _lookup_vilageRepository.GetAll() on o.VilageId equals o2.Id into j2
                        from s2 in j2.DefaultIfEmpty()

                        select new
                        {

                            o.MACLB,
                            o.TENCLB,
                            Id = o.Id,
                            StadiumTensan = s1 == null || s1.Tensan == null ? "" : s1.Tensan.ToString(),
                            Vilagetentinh = s2 == null || s2.tentinh == null ? "" : s2.tentinh.ToString()
                        };

            var totalCount = await filteredClubs.CountAsync();

            var dbList = await clubs.ToListAsync();
            var results = new List<GetClubForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClubForViewDto()
                {
                    Club = new ClubDto
                    {

                        MACLB = o.MACLB,
                        TENCLB = o.TENCLB,
                        Id = o.Id,
                    },
                    StadiumTensan = o.StadiumTensan,
                    Vilagetentinh = o.Vilagetentinh
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClubForViewDto>(
                totalCount,
                results
            );
        }

        public async Task<GetClubForViewDto> GetClubForView(int id)
        {
            var club = await _clubRepository.GetAll().FirstOrDefaultAsync(x=> x.Id == id);
            var list = await _listHLVRepository.GetAll().FirstOrDefaultAsync(x => x.MACLB == club.MACLB);
            var manager = await _managerRepository.GetAll().FirstOrDefaultAsync(x => x.Mahlv == list.Mahlv);
            var output = new GetClubForViewDto()
            {
               Club = ObjectMapper.Map<ClubDto>(club),
               List = ObjectMapper.Map<ListHLVDto>(list),
               coach = ObjectMapper.Map<ManagerDto>(manager)
            };

            if (output.Club.StadiumId != null)
            {
                var _lookupStadium = await _lookup_stadiumRepository.FirstOrDefaultAsync((int)output.Club.StadiumId);
                output.StadiumTensan = _lookupStadium?.Tensan?.ToString();
            }

            if (output.Club.VilageId != null)
            {
                var _lookupVilage = await _lookup_vilageRepository.FirstOrDefaultAsync((int)output.Club.VilageId);
                output.Vilagetentinh = _lookupVilage?.tentinh?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Clubs_Edit)]
        public async Task<GetClubForEditOutput> GetClubForEdit(EntityDto input)
        {
            var club = await _clubRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClubForEditOutput { Club = ObjectMapper.Map<CreateOrEditClubDto>(club) };

            if (output.Club.StadiumId != null)
            {
                var _lookupStadium = await _lookup_stadiumRepository.FirstOrDefaultAsync((int)output.Club.StadiumId);
                output.StadiumTensan = _lookupStadium?.Tensan?.ToString();
            }

            if (output.Club.VilageId != null)
            {
                var _lookupVilage = await _lookup_vilageRepository.FirstOrDefaultAsync((int)output.Club.VilageId);
                output.Vilagetentinh = _lookupVilage?.tentinh?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClubDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Clubs_Create)]
        protected virtual async Task Create(CreateOrEditClubDto input)
        {
            var club = ObjectMapper.Map<Club>(input);

            await _clubRepository.InsertAsync(club);

        }

        [AbpAuthorize(AppPermissions.Pages_Clubs_Edit)]
        protected virtual async Task Update(CreateOrEditClubDto input)
        {
            var club = await _clubRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, club);

        }

        [AbpAuthorize(AppPermissions.Pages_Clubs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _clubRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetClubsToExcel(GetAllClubsForExcelInput input)
        {

            var filteredClubs = _clubRepository.GetAll()
                        .Include(e => e.StadiumFk)
                        .Include(e => e.VilageFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MACLB.Contains(input.Filter) || e.TENCLB.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MACLBFilter), e => e.MACLB == input.MACLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TENCLBFilter), e => e.TENCLB == input.TENCLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StadiumTensanFilter), e => e.StadiumFk != null && e.StadiumFk.Tensan == input.StadiumTensanFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VilagetentinhFilter), e => e.VilageFk != null && e.VilageFk.tentinh == input.VilagetentinhFilter);

            var query = (from o in filteredClubs
                         join o1 in _lookup_stadiumRepository.GetAll() on o.StadiumId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_vilageRepository.GetAll() on o.VilageId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetClubForViewDto()
                         {
                             Club = new ClubDto
                             {
                                 MACLB = o.MACLB,
                                 TENCLB = o.TENCLB,
                                 Id = o.Id
                             },
                             StadiumTensan = s1 == null || s1.Tensan == null ? "" : s1.Tensan.ToString(),
                             Vilagetentinh = s2 == null || s2.tentinh == null ? "" : s2.tentinh.ToString()
                         });

            var clubListDtos = await query.ToListAsync();

            return _clubsExcelExporter.ExportToFile(clubListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Clubs)]
        public async Task<List<ClubStadiumLookupTableDto>> GetAllStadiumForTableDropdown()
        {
            return await _lookup_stadiumRepository.GetAll()
                .Select(stadium => new ClubStadiumLookupTableDto
                {
                    Id = stadium.Id,
                    DisplayName = stadium == null || stadium.Tensan == null ? "" : stadium.Tensan.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Clubs)]
        public async Task<List<ClubVilageLookupTableDto>> GetAllVilageForTableDropdown()
        {
            return await _lookup_vilageRepository.GetAll()
                .Select(vilage => new ClubVilageLookupTableDto
                {
                    Id = vilage.Id,
                    DisplayName = vilage == null || vilage.tentinh == null ? "" : vilage.tentinh.ToString()
                }).ToListAsync();
        }

    }
}