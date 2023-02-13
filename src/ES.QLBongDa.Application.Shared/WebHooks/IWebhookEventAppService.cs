using System.Threading.Tasks;
using Abp.Webhooks;

namespace ES.QLBongDa.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
