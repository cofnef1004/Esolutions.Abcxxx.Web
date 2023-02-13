using Abp.AutoMapper;
using ES.QLBongDa.Tables;
using ES.QLBongDa.Tables.Dtos;
using ES.QLBongDa.Web.Areas.App.Models.Tables;
using ES.QLBongDa.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ES.QLBongDa.Web.Areas.App.Controllers
{
    [Area("App")]
    public class TablesController : QLBongDaControllerBase
    {
        private readonly ITablesAppService _tableAppService;

        public TablesController(ITablesAppService tableAppService)
        {
            _tableAppService = tableAppService;
        }
        public IActionResult Index(GetTablesInput input)
        {
            var output = _tableAppService.GetPeople(input);
            var model = ObjectMapper.Map<IndexViewModel>(output);
            return View(model);
        }
    }
}
