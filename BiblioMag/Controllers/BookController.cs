using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BiblioMag.Models;
using BiblioMag.Models.Services;
using System;
using System.Threading.Tasks;
using System.IO;

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

        [HttpGet("{id}")]
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

        [HttpGet("AddBook")]
        public IActionResult Add()
        {
            try
            {
                var newBook = new Book();
                return View(newBook);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding a new book: " + ex.Message);
            }
        }

        [HttpPost("AddBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Book newBook, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null && file.Length > 0)
                    {
                        // Проверка типа файла
                        if (file.ContentType != "application/pdf")
                        {
                            ModelState.AddModelError("FileContent", "Загруженный файл должен быть в формате PDF.");
                            return View(newBook);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            newBook.FileContent = memoryStream.ToArray();
                        }
                    }

                    await BookService.AddBookAsync(newBook);
                    return RedirectToAction("Index");
                }

                return View(newBook);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при добавлении новой книги: " + ex.Message);
            }
        }


        [HttpPost("{id}/remove")]
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

        [HttpGet("{id}/download")]
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