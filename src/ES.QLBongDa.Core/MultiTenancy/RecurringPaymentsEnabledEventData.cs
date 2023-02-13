using Abp.Events.Bus;

namespace ES.QLBongDa.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}