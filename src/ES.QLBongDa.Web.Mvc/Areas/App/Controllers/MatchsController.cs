
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Matchs;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Matchs;
using ES.QLBongDa.Matchs.Dtos;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Rankings;
using System.Linq;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Matchs)]
    public class MatchsController : QLBongDaControllerBase
    {
        private readonly IMatchsAppService _matchsAppService;
        private readonly IRankingsAppService _rankingsAppService;

        public MatchsController(IMatchsAppService matchsAppService, IRankingsAppService rankingsAppService)
        {
            _matchsAppService = matchsAppService;
            _rankingsAppService = rankingsAppService;
        }

        public ActionResult Index()
        {
            var model = new MatchsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Matchs_Create, AppPermissions.Pages_Matchs_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            GetMatchForEditOutput getMatchForEditOutput;

            if (id.HasValue)
            {
                getMatchForEditOutput = await _matchsAppService.GetMatchForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getMatchForEditOutput = new GetMatchForEditOutput
                {
                    Match = new CreateOrEditMatchDto()
                };
            }

            var viewModel = new CreateOrEditMatchViewModel()
            {
                Match = getMatchForEditOutput.Match,
                ClubTENCLB = getMatchForEditOutput.ClubTENCLB,
                ClubTENCLB2 = getMatchForEditOutput.ClubTENCLB2,
                StadiumTensan = getMatchForEditOutput.StadiumTensan,
                MatchClubList = await _matchsAppService.GetAllClubForTableDropdown(),
                MatchStadiumList = await _matchsAppService.GetAllStadiumForTableDropdown(),
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewMatch(int id)
        {
            var getMatchForViewDto = await _matchsAppService.GetMatchForView(id);

            var model = new MatchViewModel()
            {
                Match = getMatchForViewDto.Match
                ,
                ClubTENCLB = getMatchForViewDto.ClubTENCLB

                ,
                ClubTENCLB2 = getMatchForViewDto.ClubTENCLB2

                ,
                StadiumTensan = getMatchForViewDto.StadiumTensan

            };

            return View(model);
        }

       
    }
}