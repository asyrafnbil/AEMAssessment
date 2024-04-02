using Microsoft.AspNetCore.Mvc;

namespace AEMAssessment.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}