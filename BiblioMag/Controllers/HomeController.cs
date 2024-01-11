using BiblioMag.Models.Services;
using Microsoft.AspNetCore.Mvc;
using BiblioMag.Models;
using Serilog;

namespace BiblioMag.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IBookService BookService;

        public HomeController(IBookService bookService)
        {
            BookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {   
                var books = await BookService.GetAllBooksAsync();
                if (books.Any())
                {
                    return View("Index", books);
                }
                else
                {
                    return RedirectToAction("NoBooksFound");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "При получении книг произошла ошибка: " + ex.Message);
            }
        }

        [HttpGet("NoBooksFound")]
        public IActionResult NoBooksFound()
        {
            try
            {
                return View("NoBooksFound");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "При получении книг произошла ошибка:" + ex.Message);
            }
        }
    }
}