
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Rankings;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Rankings;
using ES.QLBongDa.Rankings.Dtos;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Matchs;
using System;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Rankings)]
    public class RankingsController : QLBongDaControllerBase
    {
        private readonly IRankingsAppService _rankingsAppService;
        private readonly IMatchsAppService _matchAppService;

        public RankingsController(IRankingsAppService rankingsAppService, IMatchsAppService matchAppService)
        {
            _rankingsAppService = rankingsAppService;
            _matchAppService = matchAppService;

        }

        public ActionResult Index()
        {
            var model = new RankingsViewModel
            {
                FilterText = ""
            };
            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Rankings_Create, AppPermissions.Pages_Rankings_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            GetRankingForEditOutput getRankingForEditOutput;

            if (id.HasValue)
            {
                getRankingForEditOutput = await _rankingsAppService.GetRankingForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getRankingForEditOutput = new GetRankingForEditOutput
                {
                    Ranking = new CreateOrEditRankingDto()
                };
            }

            var viewModel = new CreateOrEditRankingViewModel()
            {
                Ranking = getRankingForEditOutput.Ranking,
                ClubTENCLB = getRankingForEditOutput.ClubTENCLB,
                RankingClubList = await _rankingsAppService.GetAllClubForTableDropdown(),
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewRanking(int id)
        {
            var getRankingForViewDto = await _rankingsAppService.GetRankingForView(id);

            var model = new RankingViewModel()
            {
                Ranking = getRankingForViewDto.Ranking
                ,
                ClubTENCLB = getRankingForViewDto.ClubTENCLB

            };

            return View(model);
        }

        public async Task<ActionResult> ViewResult(int id)
        {
            var getrs = await _matchAppService.GetMatchForView(id);
            var point = await _rankingsAppService.GetRankingForView(id);
            int first = getrs.Match.Ketqua.IndexOf("-");
            int home = Convert.ToInt32(getrs.Match.Ketqua.Substring(0, first));
            int last = getrs.Match.Ketqua.LastIndexOf("-");
            int away = Convert.ToInt32(getrs.Match.Ketqua.Substring(last + 1));
            if (point.Ranking.vong < getrs.Match.Vong)
            {
                if (home > away)
                {
                    point.Ranking.diem += 3;
                    point.Ranking.tran += 1;
                    point.Ranking.thang += 1;
                }
                if (home == away)
                {
                    point.Ranking.diem += 1;
                    point.Ranking.tran += 1;
                    point.Ranking.hoa += 1;
                }
                if (home < away)
                {
                    point.Ranking.diem += 0;
                    point.Ranking.tran += 1;
                    point.Ranking.thua += 1;
                }
            }
            var model = new RankingsViewModel
            {
                FilterText = ""
            };
            return View(model);
        }

    }
}