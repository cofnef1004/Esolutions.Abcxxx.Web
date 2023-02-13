using Microsoft.AspNetCore.Antiforgery;

namespace ES.QLBongDa.Web.Controllers
{
    public class AntiForgeryController : QLBongDaControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
