using System.Web.Mvc;

namespace Spike.PluginSpike.Api.InternalApi.ProcessB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
