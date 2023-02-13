using Microsoft.Extensions.DependencyInjection;
using ES.QLBongDa.HealthChecks;

namespace ES.QLBongDa.Web.HealthCheck
{
    public static class AbpZeroHealthCheck
    {
        public static IHealthChecksBuilder AddAbpZeroHealthCheck(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            builder.AddCheck<QLBongDaDbContextHealthCheck>("Database Connection");
            builder.AddCheck<QLBongDaDbContextUsersHealthCheck>("Database Connection with user check");
            builder.AddCheck<CacheHealthCheck>("Cache");

            // add your custom health checks here
            // builder.AddCheck<MyCustomHealthCheck>("my health check");

            return builder;
        }
    }
}
