using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Vilages;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Vilages;
using ES.QLBongDa.Vilages.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Vilages)]
    public class VilagesController : QLBongDaControllerBase
    {
        private readonly IVilagesAppService _vilagesAppService;

        public VilagesController(IVilagesAppService vilagesAppService)
        {
            _vilagesAppService = vilagesAppService;

        }

        public ActionResult Index()
        {
            var model = new VilagesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Vilages_Create, AppPermissions.Pages_Vilages_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetVilageForEditOutput getVilageForEditOutput;

            if (id.HasValue)
            {
                getVilageForEditOutput = await _vilagesAppService.GetVilageForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getVilageForEditOutput = new GetVilageForEditOutput
                {
                    Vilage = new CreateOrEditVilageDto()
                };
            }

            var viewModel = new CreateOrEditVilageModalViewModel()
            {
                Vilage = getVilageForEditOutput.Vilage,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewVilageModal(int id)
        {
            var getVilageForViewDto = await _vilagesAppService.GetVilageForView(id);

            var model = new VilageViewModel()
            {
                Vilage = getVilageForViewDto.Vilage
            };

            return PartialView("_ViewVilageModal", model);
        }

    }
}