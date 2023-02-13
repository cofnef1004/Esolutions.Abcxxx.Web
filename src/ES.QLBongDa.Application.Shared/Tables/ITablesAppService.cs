using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Tables.Dtos;

namespace ES.QLBongDa.Tables
{
    public interface ITablesAppService : IApplicationService
    {
        ListResultDto<PersonListDto> GetPeople(GetTablesInput input);
    }
}
