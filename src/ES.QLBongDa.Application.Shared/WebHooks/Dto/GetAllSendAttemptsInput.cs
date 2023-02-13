using ES.QLBongDa.Dto;

namespace ES.QLBongDa.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
