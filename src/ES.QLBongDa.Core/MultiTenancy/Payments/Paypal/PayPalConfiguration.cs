using System.Collections.Generic;
using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using ES.QLBongDa.Configuration;

namespace ES.QLBongDa.MultiTenancy.Payments.Paypal
{
    public class PayPalPaymentGatewayConfiguration : IPaymentGatewayConfiguration
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SubscriptionPaymentGatewayType GatewayType => SubscriptionPaymentGatewayType.Paypal;

        public string Environment => _appConfiguration["Payment:PayPal:Environment"];

        public string ClientId => _appConfiguration["Payment:PayPal:ClientId"];

        public string ClientSecret => _appConfiguration["Payment:PayPal:ClientSecret"];

        public string QLBongDaUsername => _appConfiguration["Payment:PayPal:QLBongDaUsername"];

        public string QLBongDaPassword => _appConfiguration["Payment:PayPal:QLBongDaPassword"];

        public bool IsActive => _appConfiguration["Payment:PayPal:IsActive"].To<bool>();

        public List<string> DisabledFundings =>
            _appConfiguration.GetSection("Payment:PayPal:DisabledFundings").Get<List<string>>();

        public bool SupportsRecurringPayments => false;

        public PayPalPaymentGatewayConfiguration(IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
