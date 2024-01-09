using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BiblioMag.Models;
using BiblioMag.Models.Services;

namespace BiblioMag.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            return View(book);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Book newBook)
        {
            if (ModelState.IsValid)
            {
                await bookService.AddBookAsync(newBook);
                return RedirectToAction("Index");
            }
            return View(newBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var success = await bookService.RemoveBookAsync(id);
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var result = await bookService.DownloadBookAsync(id);
            if (result != null)
            {
                return File(result, "application/txt", "FileNameForDownload.txt");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
