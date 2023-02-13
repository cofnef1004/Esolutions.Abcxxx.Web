using System.Threading.Tasks;
using ES.QLBongDa.MultiTenancy.Payments.Paypal;
using ES.QLBongDa.MultiTenancy.Payments.PayPal;
using ES.QLBongDa.MultiTenancy.Payments.PayPal.Dto;

namespace ES.QLBongDa.MultiTenancy.Payments
{
    public class PayPalPaymentAppService : QLBongDaAppServiceBase, IPayPalPaymentAppService
    {
        private readonly PayPalGatewayManager _payPalGatewayManager;
        private readonly ISubscriptionPaymentRepository _subscriptionPaymentRepository;
        private readonly PayPalPaymentGatewayConfiguration _payPalPaymentGatewayConfiguration;

        public PayPalPaymentAppService(
            PayPalGatewayManager payPalGatewayManager,
            ISubscriptionPaymentRepository subscriptionPaymentRepository, 
            PayPalPaymentGatewayConfiguration payPalPaymentGatewayConfiguration)
        {
            _payPalGatewayManager = payPalGatewayManager;
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _payPalPaymentGatewayConfiguration = payPalPaymentGatewayConfiguration;
        }

        public async Task ConfirmPayment(long paymentId, string paypalOrderId)
        {
            var payment = await _subscriptionPaymentRepository.GetAsync(paymentId);

            await _payPalGatewayManager.CaptureOrderAsync(
                new PayPalCaptureOrderRequestInput(paypalOrderId)
            );

            payment.Gateway = SubscriptionPaymentGatewayType.Paypal;
            payment.ExternalPaymentId = paypalOrderId;
            payment.SetAsPaid();
        }

        public PayPalConfigurationDto GetConfiguration()
        {
            return new PayPalConfigurationDto
            {
                ClientId = _payPalPaymentGatewayConfiguration.ClientId,
                QLBongDaUsername = _payPalPaymentGatewayConfiguration.QLBongDaUsername,
                QLBongDaPassword = _payPalPaymentGatewayConfiguration.QLBongDaPassword,
                DisabledFundings = _payPalPaymentGatewayConfiguration.DisabledFundings
            };
        }
    }
}