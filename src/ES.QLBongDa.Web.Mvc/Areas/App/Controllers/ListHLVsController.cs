using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Areas.App.Models.ListHLVs;
using ES.QLBongDa.Web.Controllers;
using ES.QLBongDa.Authorization;
using ES.QLBongDa.ListHLVs;
using ES.QLBongDa.ListHLVs.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_ListHLVs)]
    public class ListHLVsController : QLBongDaControllerBase
    {
        private readonly IListHLVsAppService _listHLVsAppService;

        public ListHLVsController(IListHLVsAppService listHLVsAppService)
        {
            _listHLVsAppService = listHLVsAppService;

        }

        public ActionResult Index()
        {
            var model = new ListHLVsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_ListHLVs_Create, AppPermissions.Pages_ListHLVs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetListHLVForEditOutput getListHLVForEditOutput;

            if (id.HasValue)
            {
                getListHLVForEditOutput = await _listHLVsAppService.GetListHLVForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getListHLVForEditOutput = new GetListHLVForEditOutput
                {
                    ListHLV = new CreateOrEditListHLVDto()
                };
            }

            var viewModel = new CreateOrEditListHLVModalViewModel()
            {
                ListHLV = getListHLVForEditOutput.ListHLV,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewListHLVModal(int id)
        {
            var getListHLVForViewDto = await _listHLVsAppService.GetListHLVForView(id);

            var model = new ListHLVViewModel()
            {
                ListHLV = getListHLVForViewDto.ListHLV
            };

            return PartialView("_ViewListHLVModal", model);
        }

    }
}