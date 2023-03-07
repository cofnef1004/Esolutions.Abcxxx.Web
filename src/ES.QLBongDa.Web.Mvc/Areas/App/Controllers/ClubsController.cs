﻿using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Clubs;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Clubs;
using ES.QLBongDa.Clubs.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Clubs)]
    public class ClubsController : QLBongDaControllerBase
    {
        private readonly IClubsAppService _clubsAppService;

        public ClubsController(IClubsAppService clubsAppService)
        {
            _clubsAppService = clubsAppService;

        }

        public ActionResult Index()
        {
            var model = new ClubsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Clubs_Create, AppPermissions.Pages_Clubs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetClubForEditOutput getClubForEditOutput;

            if (id.HasValue)
            {
                getClubForEditOutput = await _clubsAppService.GetClubForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getClubForEditOutput = new GetClubForEditOutput
                {
                    Club = new CreateOrEditClubDto()
                };
            }

            var viewModel = new CreateOrEditClubModalViewModel()
            {
                Club = getClubForEditOutput.Club,
                StadiumTensan = getClubForEditOutput.StadiumTensan,
                Vilagetentinh = getClubForEditOutput.Vilagetentinh,
                ClubStadiumList = await _clubsAppService.GetAllStadiumForTableDropdown(),
                ClubVilageList = await _clubsAppService.GetAllVilageForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewClubModal(int id)
        {
            var getClubForViewDto = await _clubsAppService.GetClubForView(id);

            var model = new ClubViewModel()
            {
                Club = getClubForViewDto.Club
                ,
                StadiumTensan = getClubForViewDto.StadiumTensan

                ,
                Vilagetentinh = getClubForViewDto.Vilagetentinh
                ,
                List = getClubForViewDto.List
                ,
                coach = getClubForViewDto.coach
            };

            return PartialView("_ViewClubModal", model);
        }

    }
}