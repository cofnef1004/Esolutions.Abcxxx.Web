using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Rankings;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Rankings;
using ES.QLBongDa.Rankings.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Rankings)]
    public class RankingsController : QLBongDaControllerBase
    {
        private readonly IRankingsAppService _rankingsAppService;

        public RankingsController(IRankingsAppService rankingsAppService)
        {
            _rankingsAppService = rankingsAppService;

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

    }
}