using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Tables.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}