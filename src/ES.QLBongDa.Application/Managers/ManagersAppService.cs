using ES.QLBongDa.Nations;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Managers.Exporting;
using ES.QLBongDa.Managers.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Managers
{
    [AbpAuthorize(AppPermissions.Pages_Managers)]
    public class ManagersAppService : QLBongDaAppServiceBase, IManagersAppService
    {
        private readonly IRepository<Manager> _managerRepository;
        private readonly IManagersExcelExporter _managersExcelExporter;
        private readonly IRepository<Nation, int> _lookup_nationRepository;

        public ManagersAppService(IRepository<Manager> managerRepository, IManagersExcelExporter managersExcelExporter, IRepository<Nation, int> lookup_nationRepository)
        {
            _managerRepository = managerRepository;
            _managersExcelExporter = managersExcelExporter;
            _lookup_nationRepository = lookup_nationRepository;

        }

        public async Task<PagedResultDto<GetManagerForViewDto>> GetAll(GetAllManagersInput input)
        {

            var filteredManagers = _managerRepository.GetAll()
                        .Include(e => e.NationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Mahlv.Contains(input.Filter) || e.Tenhlv.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MahlvFilter), e => e.Mahlv == input.MahlvFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenhlvFilter), e => e.Tenhlv == input.TenhlvFilter)
                        .WhereIf(input.MinNamsinhFilter != null, e => e.Namsinh >= input.MinNamsinhFilter)
                        .WhereIf(input.MaxNamsinhFilter != null, e => e.Namsinh <= input.MaxNamsinhFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NationtenqgFilter), e => e.NationFk != null && e.NationFk.tenqg == input.NationtenqgFilter);

            var pagedAndFilteredManagers = filteredManagers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var managers = from o in pagedAndFilteredManagers
                           join o1 in _lookup_nationRepository.GetAll() on o.NationId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           select new
                           {

                               o.Mahlv,
                               o.Tenhlv,
                               o.Namsinh,
                               Id = o.Id,
                               Nationtenqg = s1 == null || s1.tenqg == null ? "" : s1.tenqg.ToString()
                           };

            var totalCount = await filteredManagers.CountAsync();

            var dbList = await managers.ToListAsync();
            var results = new List<GetManagerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetManagerForViewDto()
                {
                    Manager = new ManagerDto
                    {

                        Mahlv = o.Mahlv,
                        Tenhlv = o.Tenhlv,
                        Namsinh = o.Namsinh,
                        Id = o.Id,
                    },
                    Nationtenqg = o.Nationtenqg
                };

                results.Add(res);
            }

            return new PagedResultDto<GetManagerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetManagerForViewDto> GetManagerForView(int id)
        {
            var manager = await _managerRepository.GetAsync(id);

            var output = new GetManagerForViewDto { Manager = ObjectMapper.Map<ManagerDto>(manager) };

            if (output.Manager.NationId != null)
            {
                var _lookupNation = await _lookup_nationRepository.FirstOrDefaultAsync((int)output.Manager.NationId);
                output.Nationtenqg = _lookupNation?.tenqg?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Managers_Edit)]
        public async Task<GetManagerForEditOutput> GetManagerForEdit(EntityDto input)
        {
            var manager = await _managerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetManagerForEditOutput { Manager = ObjectMapper.Map<CreateOrEditManagerDto>(manager) };

            if (output.Manager.NationId != null)
            {
                var _lookupNation = await _lookup_nationRepository.FirstOrDefaultAsync((int)output.Manager.NationId);
                output.Nationtenqg = _lookupNation?.tenqg?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditManagerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Managers_Create)]
        protected virtual async Task Create(CreateOrEditManagerDto input)
        {
            var manager = ObjectMapper.Map<Manager>(input);

            await _managerRepository.InsertAsync(manager);

        }

        [AbpAuthorize(AppPermissions.Pages_Managers_Edit)]
        protected virtual async Task Update(CreateOrEditManagerDto input)
        {
            var manager = await _managerRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, manager);

        }

        [AbpAuthorize(AppPermissions.Pages_Managers_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _managerRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetManagersToExcel(GetAllManagersForExcelInput input)
        {

            var filteredManagers = _managerRepository.GetAll()
                        .Include(e => e.NationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Mahlv.Contains(input.Filter) || e.Tenhlv.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MahlvFilter), e => e.Mahlv == input.MahlvFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenhlvFilter), e => e.Tenhlv == input.TenhlvFilter)
                        .WhereIf(input.MinNamsinhFilter != null, e => e.Namsinh >= input.MinNamsinhFilter)
                        .WhereIf(input.MaxNamsinhFilter != null, e => e.Namsinh <= input.MaxNamsinhFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NationtenqgFilter), e => e.NationFk != null && e.NationFk.tenqg == input.NationtenqgFilter);

            var query = (from o in filteredManagers
                         join o1 in _lookup_nationRepository.GetAll() on o.NationId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetManagerForViewDto()
                         {
                             Manager = new ManagerDto
                             {
                                 Mahlv = o.Mahlv,
                                 Tenhlv = o.Tenhlv,
                                 Namsinh = o.Namsinh,
                                 Id = o.Id
                             },
                             Nationtenqg = s1 == null || s1.tenqg == null ? "" : s1.tenqg.ToString()
                         });

            var managerListDtos = await query.ToListAsync();

            return _managersExcelExporter.ExportToFile(managerListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Managers)]
        public async Task<List<ManagerNationLookupTableDto>> GetAllNationForTableDropdown()
        {
            return await _lookup_nationRepository.GetAll()
                .Select(nation => new ManagerNationLookupTableDto
                {
                    Id = nation.Id,
                    DisplayName = nation == null || nation.tenqg == null ? "" : nation.tenqg.ToString()
                }).ToListAsync();
        }

    }
}