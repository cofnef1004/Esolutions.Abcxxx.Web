using Abp.Application.Services.Dto;

namespace ES.QLBongDa.ListHLVs.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}