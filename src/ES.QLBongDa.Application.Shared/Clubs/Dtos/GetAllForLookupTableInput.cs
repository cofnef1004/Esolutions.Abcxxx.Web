using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Clubs.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}