using System.Collections.Generic;
using ES.QLBongDa.Editions;
using ES.QLBongDa.Editions.Dto;
using ES.QLBongDa.MultiTenancy.Payments;
using ES.QLBongDa.MultiTenancy.Payments.Dto;

namespace ES.QLBongDa.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
