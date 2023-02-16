using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Nations.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}