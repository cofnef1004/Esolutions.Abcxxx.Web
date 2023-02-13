using Abp.Application.Services.Dto;
using Abp.Webhooks;
using ES.QLBongDa.WebHooks.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
