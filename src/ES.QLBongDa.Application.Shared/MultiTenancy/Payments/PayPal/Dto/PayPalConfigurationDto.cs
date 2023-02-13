using System.Collections.Generic;

namespace ES.QLBongDa.MultiTenancy.Payments.PayPal.Dto
{
    public class PayPalConfigurationDto
    {
        public string ClientId { get; set; }

        public string QLBongDaUsername { get; set; }

        public string QLBongDaPassword { get; set; }
        
        public List<string> DisabledFundings { get; set; }
    }
}
