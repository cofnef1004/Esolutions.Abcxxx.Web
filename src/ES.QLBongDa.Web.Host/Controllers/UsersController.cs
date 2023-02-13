using Abp.AspNetCore.Mvc.Authorization;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Storage;
using Abp.BackgroundJobs;

namespace ES.QLBongDa.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}