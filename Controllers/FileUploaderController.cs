using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace lab1.Controllers
{
    public class FileUploaderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Файл не обраний або пустий";
                return View("Index");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ViewBag.Massage = "Файл успішно завантаженно!";
            return View("Index");
        }
    }
}
