﻿using BiblioMag.Models.Services;
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
        [HttpGet("Index")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books: " + ex.Message);
            }
        }
        [HttpGet("AddBook")]
        public IActionResult AddBook()
        {
            try
            {
                Log.Information("AddBook method called");
                return RedirectToAction("Add", "Book", new { area = "" });

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
                return View("NoBooksFound");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books: " + ex.Message);
            }
        }
    }
}