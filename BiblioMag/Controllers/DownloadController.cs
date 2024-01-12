using Microsoft.AspNetCore.Mvc;
using BiblioMag.Models.Services;

namespace BiblioMag.Controllers
{
    [Route("[controller]")]
    public class DownloadController : Controller
    {
        private readonly IDownloadService DownloadService;

        public DownloadController(IDownloadService downloadService)
        {
            DownloadService = downloadService;
        }

        [HttpGet("{bookId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadBook(int bookId)
        {
            try
            {
                var book = await DownloadService.GetBookByIdAsync(bookId);

                // (доделать) логика для скачивания файла книги и возврата пользователю
                return Json(new { Message = "Your book has been downloaded", BookTitle = book.Title });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while downloading the book: " + ex.Message);
            }
        }
    }
}