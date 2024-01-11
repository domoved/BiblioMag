using BiblioMag.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace BiblioMag.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IBookService BookService;

        public HomeController(IBookService bookService)
        {
            BookService = bookService;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await BookService.GetAllBooksAsync();
                if (books.Any())
                {
                    return View(books);
                }
                else
                {
                    return RedirectToAction("NoBooksFound");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books: " + ex.Message);
            }
        }

        [HttpGet("AddBook")]
        public IActionResult AddBook()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding a new book: " + ex.Message);
            }
        }

        [HttpGet("NoBooksFound")]
        public IActionResult NoBooksFound()
        {
            try
            {
                return RedirectToAction("Add", "Book");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books: " + ex.Message);
            }
        }
    }
}