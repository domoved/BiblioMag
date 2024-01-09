using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BiblioMag.Models.Services;

namespace BiblioMag.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IDownloadService downloadService;

        public DownloadController(IDownloadService downloadService)
        {
            this.downloadService = downloadService;
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadBook(int bookId)
        {
            var book = await downloadService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            // (доделать) логика для скачивания файла книги и возврата пользователю
            return Json(new { Message = "Ваша книга скачалась", BookTitle = book.Title });
        }
    }
}