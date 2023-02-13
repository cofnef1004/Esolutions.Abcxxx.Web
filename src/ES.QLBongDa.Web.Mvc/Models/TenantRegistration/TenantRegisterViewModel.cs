using ES.QLBongDa.Editions;
using ES.QLBongDa.Editions.Dto;
using ES.QLBongDa.MultiTenancy.Payments;
using ES.QLBongDa.Security;
using ES.QLBongDa.MultiTenancy.Payments.Dto;

namespace ES.QLBongDa.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
