using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ES.QLBongDa.EntityFrameworkCore;

namespace ES.QLBongDa.HealthChecks
{
    public class QLBongDaDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public QLBongDaDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("QLBongDaDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("QLBongDaDbContext could not connect to database"));
        }
    }
}
