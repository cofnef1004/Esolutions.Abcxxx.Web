using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Nations;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Nations;
using ES.QLBongDa.Nations.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Nations)]
    public class NationsController : QLBongDaControllerBase
    {
        private readonly INationsAppService _nationsAppService;

        public NationsController(INationsAppService nationsAppService)
        {
            _nationsAppService = nationsAppService;

        }

        public ActionResult Index()
        {
            var model = new NationsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Nations_Create, AppPermissions.Pages_Nations_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            GetNationForEditOutput getNationForEditOutput;

            if (id.HasValue)
            {
                getNationForEditOutput = await _nationsAppService.GetNationForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getNationForEditOutput = new GetNationForEditOutput
                {
                    Nation = new CreateOrEditNationDto()
                };
            }

            var viewModel = new CreateOrEditNationViewModel()
            {
                Nation = getNationForEditOutput.Nation,
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewNation(int id)
        {
            var getNationForViewDto = await _nationsAppService.GetNationForView(id);

            var model = new NationViewModel()
            {
                Nation = getNationForViewDto.Nation
            };

            return View(model);
        }

    }
}