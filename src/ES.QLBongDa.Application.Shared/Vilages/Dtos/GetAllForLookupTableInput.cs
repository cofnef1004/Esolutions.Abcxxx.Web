using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Vilages.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}