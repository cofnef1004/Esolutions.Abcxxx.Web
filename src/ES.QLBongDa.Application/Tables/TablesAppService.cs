using ES.QLBongDa.Clubs;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Tables.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Tables
{
    [AbpAuthorize(AppPermissions.Pages_Tables)]
    public class TablesAppService : QLBongDaAppServiceBase, ITablesAppService
    {
        private readonly IRepository<Table> _tableRepository;
        private readonly IRepository<Club, int> _lookup_clubRepository;

        public TablesAppService(IRepository<Table> tableRepository, IRepository<Club, int> lookup_clubRepository)
        {
            _tableRepository = tableRepository;
            _lookup_clubRepository = lookup_clubRepository;

        }

        public async Task<PagedResultDto<GetTableForViewDto>> GetAll(GetAllTablesInput input)
        {

            var filteredTables = _tableRepository.GetAll()
                        .Include(e => e.maclbFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinnamFilter != null, e => e.nam >= input.MinnamFilter)
                        .WhereIf(input.MaxnamFilter != null, e => e.nam <= input.MaxnamFilter)
                        .WhereIf(input.MinvongFilter != null, e => e.vong >= input.MinvongFilter)
                        .WhereIf(input.MaxvongFilter != null, e => e.vong <= input.MaxvongFilter)
                        .WhereIf(input.MinsotranFilter != null, e => e.sotran >= input.MinsotranFilter)
                        .WhereIf(input.MaxsotranFilter != null, e => e.sotran <= input.MaxsotranFilter)
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

            var pagedAndFilteredTables = filteredTables
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var tables = from o in pagedAndFilteredTables
                         join o1 in _lookup_clubRepository.GetAll() on o.maclb equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new
                         {

                             o.nam,
                             o.vong,
                             o.sotran,
                             o.thang,
                             o.hoa,
                             o.thua,
                             o.hieuso,
                             o.diem,
                             Id = o.Id,
                             ClubTENCLB = s1 == null || s1.TENCLB == null ? "" : s1.TENCLB.ToString()
                         };

            var totalCount = await filteredTables.CountAsync();

            var dbList = await tables.ToListAsync();
            var results = new List<GetTableForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTableForViewDto()
                {
                    Table = new TableDto
                    {

                        nam = o.nam,
                        vong = o.vong,
                        sotran = o.sotran,
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

            return new PagedResultDto<GetTableForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTableForViewDto> GetTableForView(int id)
        {
            var table = await _tableRepository.GetAsync(id);

            var output = new GetTableForViewDto { Table = ObjectMapper.Map<TableDto>(table) };

            if (output.Table.maclb != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Table.maclb);
                output.ClubTENCLB = _lookupClub?.TENCLB?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Tables_Edit)]
        public async Task<GetTableForEditOutput> GetTableForEdit(EntityDto input)
        {
            var table = await _tableRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTableForEditOutput { Table = ObjectMapper.Map<CreateOrEditTableDto>(table) };

            if (output.Table.maclb != null)
            {
                var _lookupClub = await _lookup_clubRepository.FirstOrDefaultAsync((int)output.Table.maclb);
                output.ClubTENCLB = _lookupClub?.TENCLB?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTableDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Tables_Create)]
        protected virtual async Task Create(CreateOrEditTableDto input)
        {
            var table = ObjectMapper.Map<Table>(input);

            await _tableRepository.InsertAsync(table);

        }

        [AbpAuthorize(AppPermissions.Pages_Tables_Edit)]
        protected virtual async Task Update(CreateOrEditTableDto input)
        {
            var table = await _tableRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, table);

        }

        [AbpAuthorize(AppPermissions.Pages_Tables_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _tableRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Tables)]
        public async Task<List<TableClubLookupTableDto>> GetAllClubForTableDropdown()
        {
            return await _lookup_clubRepository.GetAll()
                .Select(club => new TableClubLookupTableDto
                {
                    Id = club.Id,
                    DisplayName = club == null || club.TENCLB == null ? "" : club.TENCLB.ToString()
                }).ToListAsync();
        }

    }
}