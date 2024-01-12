using Microsoft.AspNetCore.Mvc;
using BiblioMag.Models;
using BiblioMag.Models.Services;

namespace BiblioMag.Controllers
{
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService BookService;

        public BookController(IBookService bookService)
        {
            BookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await BookService.GetAllBooksAsync();
                return View("Index", books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "При получении книг произошла ошибка: " + ex.Message);
            }
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var book = await BookService.GetBookByIdAsync(id);
                return View("Details", book);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "При получении сведений о книге произошла ошибка: " + ex.Message);
            }
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Book newBook, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file is { Length: > 0 })
                    {
                        if (file.ContentType != "application/pdf")
                        {
                            ModelState.AddModelError("FileContent", "Загруженный файл должен быть в формате PDF.");
                            return View("Add", newBook);
                        }

                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);
                        newBook.FileContent = memoryStream.ToArray();
                    }

                    await BookService.AddBookAsync(newBook);
                    TempData["SuccessMessage"] = "Книга успешно загружена";
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = "Произошла ошибка при загрузке книги";
                return View("Add", newBook);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при добавлении новой книги: " + ex.Message);
            }
        }

        [HttpPost("Remove/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                if (await BookService.RemoveBookAsync(id))
                {
                    return RedirectToAction("Index");
                }

                return NotFound("Книга не найдена");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "При удалении книги произошла ошибка: " + ex.Message);
            }
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var result = await DownloadService.DownloadBookAsync(id);
                if (result != null)
                {
                    return File(result, "application/pdf", "FileDownloadName.pdf");
                }

                return NotFound("Книга не найдена");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "При загрузке книги произошла ошибка: " + ex.Message);
            }
        }
    }
}