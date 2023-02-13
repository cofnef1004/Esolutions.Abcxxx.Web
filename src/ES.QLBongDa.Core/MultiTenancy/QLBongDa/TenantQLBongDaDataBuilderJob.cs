using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;

namespace ES.QLBongDa.MultiTenancy.QLBongDa
{
    public class TenantQLBongDaDataBuilderJob : AsyncBackgroundJob<int>, ITransientDependency
    {
        private readonly TenantQLBongDaDataBuilder _tenantQLBongDaDataBuilder;
        private readonly TenantManager _tenantManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TenantQLBongDaDataBuilderJob(
            TenantQLBongDaDataBuilder tenantQLBongDaDataBuilder, 
            TenantManager tenantManager, 
            IUnitOfWorkManager unitOfWorkManager)
        {
            _tenantQLBongDaDataBuilder = tenantQLBongDaDataBuilder;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public override async Task ExecuteAsync(int args)
        {
            var tenantId = args;
            var tenant = await _tenantManager.GetByIdAsync(tenantId);
            using (var uow = _unitOfWorkManager.Begin())
            {
                await _tenantQLBongDaDataBuilder.BuildForAsync(tenant);
                await uow.CompleteAsync();
            }
        }
    }
}
