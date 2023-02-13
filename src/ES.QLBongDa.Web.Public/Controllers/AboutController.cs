using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Controllers;

namespace ES.QLBongDa.Web.Public.Controllers
{
    public class AboutController : QLBongDaControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}