using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Stadiums;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Stadiums;
using ES.QLBongDa.Stadiums.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Stadiums)]
    public class StadiumsController : QLBongDaControllerBase
    {
        private readonly IStadiumsAppService _stadiumsAppService;

        public StadiumsController(IStadiumsAppService stadiumsAppService)
        {
            _stadiumsAppService = stadiumsAppService;

        }

        public ActionResult Index()
        {
            var model = new StadiumsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Stadiums_Create, AppPermissions.Pages_Stadiums_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            GetStadiumForEditOutput getStadiumForEditOutput;

            if (id.HasValue)
            {
                getStadiumForEditOutput = await _stadiumsAppService.GetStadiumForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getStadiumForEditOutput = new GetStadiumForEditOutput
                {
                    Stadium = new CreateOrEditStadiumDto()
                };
            }

            var viewModel = new CreateOrEditStadiumViewModel()
            {
                Stadium = getStadiumForEditOutput.Stadium,
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewStadium(int id)
        {
            var getStadiumForViewDto = await _stadiumsAppService.GetStadiumForView(id);

            var model = new StadiumViewModel()
            {
                Stadium = getStadiumForViewDto.Stadium
            };

            return View(model);
        }

    }
}