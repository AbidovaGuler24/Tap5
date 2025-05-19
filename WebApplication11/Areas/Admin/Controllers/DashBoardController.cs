using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication11.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("Admin")]

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
