using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Tables.Dtos;
using ES.QLBongDa.Dto;
using System.Collections.Generic;

namespace ES.QLBongDa.Tables
{
    public interface ITablesAppService : IApplicationService
    {
        Task<PagedResultDto<GetTableForViewDto>> GetAll(GetAllTablesInput input);

        Task<GetTableForViewDto> GetTableForView(int id);

        Task<GetTableForEditOutput> GetTableForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditTableDto input);

        Task Delete(EntityDto input);

        Task<List<TableClubLookupTableDto>> GetAllClubForTableDropdown();

    }
}