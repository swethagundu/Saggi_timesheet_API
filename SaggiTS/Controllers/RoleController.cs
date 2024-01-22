using Microsoft.AspNetCore.Mvc;

namespace SaggiTS.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
