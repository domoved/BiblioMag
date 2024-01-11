using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BiblioMag.Models.Services;
using System;

namespace BiblioMag.Controllers
{
    [Route("[controller]")]
    public class ReadingController : Controller
    {
        private readonly IReadingService ReadingService;

        public ReadingController(IReadingService readingService)
        {
            ReadingService = readingService;
        }

        [HttpPost("{bookId}/start-reading")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartReading(int bookId)
        {
            try
            {
                await ReadingService.StartReadingAsync(bookId);
                return Json(new { Message = "Reading started", StartTime = DateTime.Now });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while starting reading: " + ex.Message);
            }
        }

        [HttpPost("{bookId}/end-reading")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EndReading(int bookId)
        {
            try
            {
                await ReadingService.EndReadingAsync(bookId);
                return Json(new { Message = "Reading ended", EndTime = DateTime.Now });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while ending reading: " + ex.Message);
            }
        }
    }
}