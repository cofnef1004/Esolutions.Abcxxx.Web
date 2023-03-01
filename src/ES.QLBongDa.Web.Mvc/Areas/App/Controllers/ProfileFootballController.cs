using System.Threading.Tasks;
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

        public ProfileFootballController(IRankingsAppService rankingsAppService, IMatchsAppService matchAppService)
        {
            _rankingsAppService = rankingsAppService;
            _matchAppService = matchAppService;
        }
        public  IActionResult Index()
        {
            /*var getRankingForViewDto = await _rankingsAppService.GetRankingForView(id);*/
            var model = new ProfileFootballViewModel
            {
                TotalMatch = /*getRankingForViewDto.Ranking.tran*/98
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
