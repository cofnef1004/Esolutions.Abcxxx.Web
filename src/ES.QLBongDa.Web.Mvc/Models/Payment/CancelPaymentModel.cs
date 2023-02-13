using ES.QLBongDa.MultiTenancy.Payments;

namespace ES.QLBongDa.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}