using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.DAL;
using WebApplication11.Helpers.Exictence;
using WebApplication11.Models;
using WebApplication11.ViewModels;

namespace WebApplication11.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VillaController : Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _environment;

        public VillaController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var villaVm = await _context.Villas.ToListAsync();
            return View(villaVm);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VillaVm villaVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (villaVm.File.Length>25000000)
            {

            }
            if (!villaVm.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "File duzgun elave et");
                return View();
            }
            Villa villa = new Villa()
            {
               Price = villaVm.Price,
               Title = villaVm.Title,

                ImgUrl = FileCreateExtension.CreateFile(villaVm.File, _environment.WebRootPath, "Upload/Villa")

            };

            await _context.AddAsync(villa);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update( int? id )
        {
            if (id == null)
            {
                return BadRequest();

            }
            var dbvilla = await _context.Villas.FindAsync(id);
            if (dbvilla == null)
            {
                return NotFound();
            }
            VillaVm villaVm = new VillaVm()
            {
                Price = dbvilla.Price,
                Title = dbvilla.Title,
                Id=dbvilla.Id,

              ImgUrl=dbvilla.ImgUrl,
            };

            return View(villaVm);

        }
        [HttpPost]
        public async Task<IActionResult> Update(VillaVm villaVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (villaVm.Id == null)
            {
                return BadRequest();
            }
            var dbvilla = await _context.Villas.FindAsync(villaVm.Id);
            if (dbvilla == null)

            {
                return NotFound();
            }

            if (villaVm.File != null)
            {
                if (villaVm.File.Length > 2500000)
                {
                    ModelState.AddModelError("File", "uzunluq sehfdir");
                    return View();

                }
                if (!villaVm.File.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("File", "File duzgun elave et");
                    return View();
                }

                FileCreateExtension.RemoveFile(villaVm.File, _environment.WebRootPath.ToString(), "Upload/Villa");

                dbvilla.ImgUrl = FileCreateExtension.CreateFile(villaVm.File, _environment.WebRootPath, "Upload/Villa");

              
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            dbvilla.Price = villaVm.Price;
            dbvilla.ImgUrl = villaVm.ImgUrl;
            dbvilla.Title = villaVm.Title;  

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var chef = await _context.Villas.FindAsync(id);
            if (chef == null)
            {
                return BadRequest();
            }
            _context.Villas.Remove(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
