using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Vilages.Exporting;
using ES.QLBongDa.Vilages.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Vilages
{
    [AbpAuthorize(AppPermissions.Pages_Vilages)]
    public class VilagesAppService : QLBongDaAppServiceBase, IVilagesAppService
    {
        private readonly IRepository<Vilage> _vilageRepository;
        private readonly IVilagesExcelExporter _vilagesExcelExporter;

        public VilagesAppService(IRepository<Vilage> vilageRepository, IVilagesExcelExporter vilagesExcelExporter)
        {
            _vilageRepository = vilageRepository;
            _vilagesExcelExporter = vilagesExcelExporter;

        }

        public async Task<PagedResultDto<GetVilageForViewDto>> GetAll(GetAllVilagesInput input)
        {

            var filteredVilages = _vilageRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.matinh.Contains(input.Filter) || e.tentinh.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.matinhFilter), e => e.matinh == input.matinhFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.tentinhFilter), e => e.tentinh == input.tentinhFilter);

            var pagedAndFilteredVilages = filteredVilages
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var vilages = from o in pagedAndFilteredVilages
                          select new
                          {

                              o.matinh,
                              o.tentinh,
                              Id = o.Id
                          };

            var totalCount = await filteredVilages.CountAsync();

            var dbList = await vilages.ToListAsync();
            var results = new List<GetVilageForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetVilageForViewDto()
                {
                    Vilage = new VilageDto
                    {

                        matinh = o.matinh,
                        tentinh = o.tentinh,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetVilageForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetVilageForViewDto> GetVilageForView(int id)
        {
            var vilage = await _vilageRepository.GetAsync(id);

            var output = new GetVilageForViewDto { Vilage = ObjectMapper.Map<VilageDto>(vilage) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Vilages_Edit)]
        public async Task<GetVilageForEditOutput> GetVilageForEdit(EntityDto input)
        {
            var vilage = await _vilageRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetVilageForEditOutput { Vilage = ObjectMapper.Map<CreateOrEditVilageDto>(vilage) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditVilageDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Vilages_Create)]
        protected virtual async Task Create(CreateOrEditVilageDto input)
        {
            var vilage = ObjectMapper.Map<Vilage>(input);

            await _vilageRepository.InsertAsync(vilage);

        }

        [AbpAuthorize(AppPermissions.Pages_Vilages_Edit)]
        protected virtual async Task Update(CreateOrEditVilageDto input)
        {
            var vilage = await _vilageRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, vilage);

        }

        [AbpAuthorize(AppPermissions.Pages_Vilages_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _vilageRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetVilagesToExcel(GetAllVilagesForExcelInput input)
        {

            var filteredVilages = _vilageRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.matinh.Contains(input.Filter) || e.tentinh.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.matinhFilter), e => e.matinh == input.matinhFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.tentinhFilter), e => e.tentinh == input.tentinhFilter);

            var query = (from o in filteredVilages
                         select new GetVilageForViewDto()
                         {
                             Vilage = new VilageDto
                             {
                                 matinh = o.matinh,
                                 tentinh = o.tentinh,
                                 Id = o.Id
                             }
                         });

            var vilageListDtos = await query.ToListAsync();

            return _vilagesExcelExporter.ExportToFile(vilageListDtos);
        }

    }
}