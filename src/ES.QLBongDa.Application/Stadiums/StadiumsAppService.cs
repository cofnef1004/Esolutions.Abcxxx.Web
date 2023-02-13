using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.Stadiums.Exporting;
using ES.QLBongDa.Stadiums.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Stadiums
{
    [AbpAuthorize(AppPermissions.Pages_Stadiums)]
    public class StadiumsAppService : QLBongDaAppServiceBase, IStadiumsAppService
    {
        private readonly IRepository<Stadium> _stadiumRepository;
        private readonly IStadiumsExcelExporter _stadiumsExcelExporter;

        public StadiumsAppService(IRepository<Stadium> stadiumRepository, IStadiumsExcelExporter stadiumsExcelExporter)
        {
            _stadiumRepository = stadiumRepository;
            _stadiumsExcelExporter = stadiumsExcelExporter;

        }

        public async Task<PagedResultDto<GetStadiumForViewDto>> GetAll(GetAllStadiumsInput input)
        {

            var filteredStadiums = _stadiumRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Masan.Contains(input.Filter) || e.Tensan.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MasanFilter), e => e.Masan == input.MasanFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TensanFilter), e => e.Tensan == input.TensanFilter);

            var pagedAndFilteredStadiums = filteredStadiums
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var stadiums = from o in pagedAndFilteredStadiums
                           select new
                           {

                               o.Masan,
                               o.Tensan,
                               Id = o.Id
                           };

            var totalCount = await filteredStadiums.CountAsync();

            var dbList = await stadiums.ToListAsync();
            var results = new List<GetStadiumForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetStadiumForViewDto()
                {
                    Stadium = new StadiumDto
                    {

                        Masan = o.Masan,
                        Tensan = o.Tensan,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetStadiumForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetStadiumForViewDto> GetStadiumForView(int id)
        {
            var stadium = await _stadiumRepository.GetAsync(id);

            var output = new GetStadiumForViewDto { Stadium = ObjectMapper.Map<StadiumDto>(stadium) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Stadiums_Edit)]
        public async Task<GetStadiumForEditOutput> GetStadiumForEdit(EntityDto input)
        {
            var stadium = await _stadiumRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetStadiumForEditOutput { Stadium = ObjectMapper.Map<CreateOrEditStadiumDto>(stadium) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditStadiumDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Stadiums_Create)]
        protected virtual async Task Create(CreateOrEditStadiumDto input)
        {
            var stadium = ObjectMapper.Map<Stadium>(input);

            await _stadiumRepository.InsertAsync(stadium);

        }

        [AbpAuthorize(AppPermissions.Pages_Stadiums_Edit)]
        protected virtual async Task Update(CreateOrEditStadiumDto input)
        {
            var stadium = await _stadiumRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, stadium);

        }

        [AbpAuthorize(AppPermissions.Pages_Stadiums_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _stadiumRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetStadiumsToExcel(GetAllStadiumsForExcelInput input)
        {

            var filteredStadiums = _stadiumRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Masan.Contains(input.Filter) || e.Tensan.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MasanFilter), e => e.Masan == input.MasanFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TensanFilter), e => e.Tensan == input.TensanFilter);

            var query = (from o in filteredStadiums
                         select new GetStadiumForViewDto()
                         {
                             Stadium = new StadiumDto
                             {
                                 Masan = o.Masan,
                                 Tensan = o.Tensan,
                                 Id = o.Id
                             }
                         });

            var stadiumListDtos = await query.ToListAsync();

            return _stadiumsExcelExporter.ExportToFile(stadiumListDtos);
        }

    }
}