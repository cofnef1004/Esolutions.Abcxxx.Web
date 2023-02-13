using System.Threading.Tasks;
using Abp.Application.Services;
using ES.QLBongDa.MultiTenancy.Payments.PayPal.Dto;

namespace ES.QLBongDa.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
