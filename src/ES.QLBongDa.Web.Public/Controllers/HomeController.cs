using Microsoft.AspNetCore.Mvc;
using ES.QLBongDa.Web.Controllers;

namespace ES.QLBongDa.Web.Public.Controllers
{
    public class HomeController : QLBongDaControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}