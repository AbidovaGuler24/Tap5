using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.DAL;

namespace WebApplication11.Controllers
{
    [Authorize(Roles ="Admin,Member")]
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
