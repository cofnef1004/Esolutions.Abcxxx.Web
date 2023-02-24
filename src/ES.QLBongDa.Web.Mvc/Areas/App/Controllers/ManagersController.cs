using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.Managers;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.Managers;
using ES.QLBongDa.Managers.Dtos;
using Abp.Application.Services.Dto;


namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Managers)]
    public class ManagersController : QLBongDaControllerBase
    {
        private readonly IManagersAppService _managersAppService;

        public ManagersController(IManagersAppService managersAppService)
        {
            _managersAppService = managersAppService;

        }

        public ActionResult Index()
        {
            var model = new ManagersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Managers_Create, AppPermissions.Pages_Managers_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            GetManagerForEditOutput getManagerForEditOutput;

            if (id.HasValue)
            {
                getManagerForEditOutput = await _managersAppService.GetManagerForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getManagerForEditOutput = new GetManagerForEditOutput
                {
                    Manager = new CreateOrEditManagerDto()
                };
            }

            var viewModel = new CreateOrEditManagerViewModel()
            {
                Manager = getManagerForEditOutput.Manager,
                Nationtenqg = getManagerForEditOutput.Nationtenqg,
                ManagerNationList = await _managersAppService.GetAllNationForTableDropdown(),
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewManager(int id)
        {
            var getManagerForViewDto = await _managersAppService.GetManagerForView(id);


            var model = new ManagerViewModel()
            {
                Manager = getManagerForViewDto.Manager
                ,
                Nationtenqg = getManagerForViewDto.Nationtenqg
                ,
                ClubForViewDto = getManagerForViewDto.ClubForViewDto
            };

            return View(model);
        }
    }
}