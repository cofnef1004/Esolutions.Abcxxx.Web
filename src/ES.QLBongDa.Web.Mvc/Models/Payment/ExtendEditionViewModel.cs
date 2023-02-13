using System.Collections.Generic;
using ES.QLBongDa.Editions.Dto;
using ES.QLBongDa.MultiTenancy.Payments;

namespace ES.QLBongDa.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}