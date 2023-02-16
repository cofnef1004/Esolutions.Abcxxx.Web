using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Nations.Exporting;
using ES.QLBongDa.Nations.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Nations
{
    [AbpAuthorize(AppPermissions.Pages_Nations)]
    public class NationsAppService : QLBongDaAppServiceBase, INationsAppService
    {
        private readonly IRepository<Nation> _nationRepository;
        private readonly INationsExcelExporter _nationsExcelExporter;

        public NationsAppService(IRepository<Nation> nationRepository, INationsExcelExporter nationsExcelExporter)
        {
            _nationRepository = nationRepository;
            _nationsExcelExporter = nationsExcelExporter;

        }

        public async Task<PagedResultDto<GetNationForViewDto>> GetAll(GetAllNationsInput input)
        {

            var filteredNations = _nationRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.maqg.Contains(input.Filter) || e.tenqg.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.maqgFilter), e => e.maqg == input.maqgFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.tenqgFilter), e => e.tenqg == input.tenqgFilter);

            var pagedAndFilteredNations = filteredNations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var nations = from o in pagedAndFilteredNations
                          select new
                          {

                              o.maqg,
                              o.tenqg,
                              Id = o.Id
                          };

            var totalCount = await filteredNations.CountAsync();

            var dbList = await nations.ToListAsync();
            var results = new List<GetNationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetNationForViewDto()
                {
                    Nation = new NationDto
                    {

                        maqg = o.maqg,
                        tenqg = o.tenqg,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetNationForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetNationForViewDto> GetNationForView(int id)
        {
            var nation = await _nationRepository.GetAsync(id);

            var output = new GetNationForViewDto { Nation = ObjectMapper.Map<NationDto>(nation) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Nations_Edit)]
        public async Task<GetNationForEditOutput> GetNationForEdit(EntityDto input)
        {
            var nation = await _nationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetNationForEditOutput { Nation = ObjectMapper.Map<CreateOrEditNationDto>(nation) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditNationDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Nations_Create)]
        protected virtual async Task Create(CreateOrEditNationDto input)
        {
            var nation = ObjectMapper.Map<Nation>(input);

            await _nationRepository.InsertAsync(nation);

        }

        [AbpAuthorize(AppPermissions.Pages_Nations_Edit)]
        protected virtual async Task Update(CreateOrEditNationDto input)
        {
            var nation = await _nationRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, nation);

        }

        [AbpAuthorize(AppPermissions.Pages_Nations_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _nationRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetNationsToExcel(GetAllNationsForExcelInput input)
        {

            var filteredNations = _nationRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.maqg.Contains(input.Filter) || e.tenqg.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.maqgFilter), e => e.maqg == input.maqgFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.tenqgFilter), e => e.tenqg == input.tenqgFilter);

            var query = (from o in filteredNations
                         select new GetNationForViewDto()
                         {
                             Nation = new NationDto
                             {
                                 maqg = o.maqg,
                                 tenqg = o.tenqg,
                                 Id = o.Id
                             }
                         });

            var nationListDtos = await query.ToListAsync();

            return _nationsExcelExporter.ExportToFile(nationListDtos);
        }

    }
}