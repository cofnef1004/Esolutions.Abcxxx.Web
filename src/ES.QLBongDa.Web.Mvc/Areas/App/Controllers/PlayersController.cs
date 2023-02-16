using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Players;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Players;
using ES.QLBongDa.Players.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Players)]
    public class PlayersController : QLBongDaControllerBase
    {
        private readonly IPlayersAppService _playersAppService;

        public PlayersController(IPlayersAppService playersAppService)
        {
            _playersAppService = playersAppService;

        }

        public ActionResult Index()
        {
            var model = new PlayersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Players_Create, AppPermissions.Pages_Players_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            GetPlayerForEditOutput getPlayerForEditOutput;

            if (id.HasValue)
            {
                getPlayerForEditOutput = await _playersAppService.GetPlayerForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getPlayerForEditOutput = new GetPlayerForEditOutput
                {
                    Player = new CreateOrEditPlayerDto()
                };
            }

            var viewModel = new CreateOrEditPlayerViewModel()
            {
                Player = getPlayerForEditOutput.Player,
                ClubMACLB = getPlayerForEditOutput.ClubMACLB,
                Nationmaqg = getPlayerForEditOutput.Nationmaqg,
                PlayerClubList = await _playersAppService.GetAllClubForTableDropdown(),
                PlayerNationList = await _playersAppService.GetAllNationForTableDropdown(),
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewPlayer(int id)
        {
            var getPlayerForViewDto = await _playersAppService.GetPlayerForView(id);

            var model = new PlayerViewModel()
            {
                Player = getPlayerForViewDto.Player
                ,
                ClubMACLB = getPlayerForViewDto.ClubMACLB

                ,
                Nationmaqg = getPlayerForViewDto.Nationmaqg

            };

            return View(model);
        }

    }
}