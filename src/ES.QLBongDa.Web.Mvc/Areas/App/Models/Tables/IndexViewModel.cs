using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ES.QLBongDa.Tables.Dtos;

namespace ES.QLBongDa.Web.Areas.App.Models.Tables
{
    [AutoMapFrom(typeof(ListResultDto<PersonListDto>))]
    public class IndexViewModel : ListResultDto<PersonListDto>
    {
    }
}
