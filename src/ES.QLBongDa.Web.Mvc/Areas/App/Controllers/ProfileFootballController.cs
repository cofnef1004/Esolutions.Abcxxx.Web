using System.Linq;
using System.Threading.Tasks;
using ES.QLBongDa.Clubs;
using ES.QLBongDa.Matchs;
using ES.QLBongDa.Rankings;
using ES.QLBongDa.Web.Areas.App.Models.Rankings;
using ES.QLBongDa.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    public class ProfileFootballController : QLBongDaControllerBase
    {
        private readonly IRankingsAppService _rankingsAppService;
        private readonly IMatchsAppService _matchAppService;
        private readonly IClubsAppService _clubAppService;

        public ProfileFootballController(IRankingsAppService rankingsAppService, IClubsAppService clubAppService, IMatchsAppService matchAppService)
        {
            _rankingsAppService = rankingsAppService;
            _matchAppService = matchAppService;
            _clubAppService = clubAppService;
        }
        public async Task<ActionResult> Index(int id)
        {
            var getInforView = await _clubAppService.GetClubForView(id);
            /*var getRankingForViewDto = await _rankingsAppService.GetRankingForView(id);*/
            var model = new ProfileFootballViewModel
            {
                TotalMatch = /*getInforView.Club.MACLB.Count()*/94
                ,
                MatchDone = 4
                ,
                MatchAhead= 94
                ,
                MostGoalsClub= "CLB Hà Nội"
                ,
                LeastGoalsClub = "Hồng Lĩnh Hà Tĩnh"
                ,
                BestSave = "CLB Hà Nội"
                ,
                LeastSave = "Hồng Lĩnh Hà Tĩnh"

            };
            return View(model);
        }
    }
}
