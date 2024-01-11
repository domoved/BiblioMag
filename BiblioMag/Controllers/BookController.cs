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
                return View(books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books: " + ex.Message);
            }
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var book = await BookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound("Book not found");
                }
                return View(book);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving book details: " + ex.Message);
            }
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
                            return View(newBook);
                        }

                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);
                        newBook.FileContent = memoryStream.ToArray();
                    }

                    await BookService.AddBookAsync(newBook);
                }
                return RedirectToAction("Index", "Home");
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
                var success = await BookService.RemoveBookAsync(id);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Book not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the book: " + ex.Message);
            }
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var result = await BookService.DownloadBookAsync(id);
                if (result != null)
                {
                    return File(result, "application/pdf", "FileDownloadName.pdf");
                }
                else
                {
                    return NotFound("Book not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while downloading the book: " + ex.Message);
            }
        }
    }
}