using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Players.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}