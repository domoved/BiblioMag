using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BiblioMag.Models.Services;

namespace BiblioMag.Controllers
{
    public class ReadingController : Controller
    {
        private readonly IReadingService readingService;

        public ReadingController(IReadingService readingService)
        {
            this.readingService = readingService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartReading(int bookId)
        {
            await readingService.StartReadingAsync(bookId);
            return Json(new { Message = "Reading started", StartTime = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EndReading(int bookId)
        {
            await readingService.EndReadingAsync(bookId);
            return Json(new { Message = "Reading ended", EndTime = DateTime.Now });
        }
    }
}