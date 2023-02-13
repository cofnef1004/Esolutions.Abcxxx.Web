using System.Threading.Tasks;
using Abp.Application.Services;
using ES.QLBongDa.MultiTenancy.Payments.Dto;
using ES.QLBongDa.MultiTenancy.Payments.Stripe.Dto;

namespace ES.QLBongDa.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}