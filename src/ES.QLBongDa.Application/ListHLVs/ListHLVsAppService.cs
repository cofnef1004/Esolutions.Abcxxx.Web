using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ES.QLBongDa.ListHLVs.Dtos;
using ES.QLBongDa.Dto;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.ListHLVs
{
    [AbpAuthorize(AppPermissions.Pages_ListHLVs)]
    public class ListHLVsAppService : QLBongDaAppServiceBase, IListHLVsAppService
    {
        private readonly IRepository<ListHLV> _listHLVRepository;

        public ListHLVsAppService(IRepository<ListHLV> listHLVRepository)
        {
            _listHLVRepository = listHLVRepository;

        }

        public async Task<PagedResultDto<GetListHLVForViewDto>> GetAll(GetAllListHLVsInput input)
        {

            var filteredListHLVs = _listHLVRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Mahlv.Contains(input.Filter) || e.MACLB.Contains(input.Filter) || e.VAITRO.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MahlvFilter), e => e.Mahlv == input.MahlvFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MACLBFilter), e => e.MACLB == input.MACLBFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VAITROFilter), e => e.VAITRO == input.VAITROFilter);

            var pagedAndFilteredListHLVs = filteredListHLVs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var listHLVs = from o in pagedAndFilteredListHLVs
                           select new
                           {

                               o.Mahlv,
                               o.MACLB,
                               o.VAITRO,
                               Id = o.Id
                           };

            var totalCount = await filteredListHLVs.CountAsync();

            var dbList = await listHLVs.ToListAsync();
            var results = new List<GetListHLVForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetListHLVForViewDto()
                {
                    ListHLV = new ListHLVDto
                    {

                        Mahlv = o.Mahlv,
                        MACLB = o.MACLB,
                        VAITRO = o.VAITRO,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetListHLVForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetListHLVForViewDto> GetListHLVForView(int id)
        {
            var listHLV = await _listHLVRepository.GetAsync(id);

            var output = new GetListHLVForViewDto { ListHLV = ObjectMapper.Map<ListHLVDto>(listHLV) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ListHLVs_Edit)]
        public async Task<GetListHLVForEditOutput> GetListHLVForEdit(EntityDto input)
        {
            var listHLV = await _listHLVRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetListHLVForEditOutput { ListHLV = ObjectMapper.Map<CreateOrEditListHLVDto>(listHLV) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditListHLVDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ListHLVs_Create)]
        protected virtual async Task Create(CreateOrEditListHLVDto input)
        {
            var listHLV = ObjectMapper.Map<ListHLV>(input);

            await _listHLVRepository.InsertAsync(listHLV);

        }

        [AbpAuthorize(AppPermissions.Pages_ListHLVs_Edit)]
        protected virtual async Task Update(CreateOrEditListHLVDto input)
        {
            var listHLV = await _listHLVRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, listHLV);

        }

        [AbpAuthorize(AppPermissions.Pages_ListHLVs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _listHLVRepository.DeleteAsync(input.Id);
        }

    }
}