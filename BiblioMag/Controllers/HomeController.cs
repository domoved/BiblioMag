using BiblioMag.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiblioMag.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;

        public HomeController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            var books = await bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // (доделать) логика для скачивания файла книги и возврата пользователю
            return Json(new { Message = "Ваша книга скачалась", BookTitle = book.Title });
        }
    }
}