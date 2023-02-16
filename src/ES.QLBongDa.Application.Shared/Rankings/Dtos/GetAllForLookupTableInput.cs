using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Rankings.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}