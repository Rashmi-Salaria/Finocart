using Microsoft.AspNetCore.Mvc;

namespace Finocart.Web.Controllers
{
    public class FinocartMasterController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}