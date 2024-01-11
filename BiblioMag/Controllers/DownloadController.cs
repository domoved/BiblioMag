using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BiblioMag.Models.Services;
using System;

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

        [HttpGet("{bookId}/download")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadBook(int bookId)
        {
            try
            {
                var book = await DownloadService.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    return NotFound("Book not found");
                }

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