using System.Threading.Tasks;
using ES.QLBongDa.Authorization.Users;

namespace ES.QLBongDa.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
