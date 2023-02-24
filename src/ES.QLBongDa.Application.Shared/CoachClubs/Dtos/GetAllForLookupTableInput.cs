using Abp.Application.Services.Dto;

namespace ES.QLBongDa.CoachClubs.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}