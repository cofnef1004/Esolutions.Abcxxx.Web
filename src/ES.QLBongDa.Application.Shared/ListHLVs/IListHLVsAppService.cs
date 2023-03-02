using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.ListHLVs.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.ListHLVs
{
    public interface IListHLVsAppService : IApplicationService
    {
        Task<PagedResultDto<GetListHLVForViewDto>> GetAll(GetAllListHLVsInput input);

        Task<GetListHLVForViewDto> GetListHLVForView(int id);

        Task<GetListHLVForEditOutput> GetListHLVForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditListHLVDto input);

        Task Delete(EntityDto input);

    }
}