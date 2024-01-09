using Microsoft.EntityFrameworkCore;

namespace BiblioMag.Models.Services
{
    public class ReadingService : IReadingService
    {
        private readonly IBookRepository bookRepository;

        public ReadingService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task StartReadingAsync(int bookId)
        {
            var book = await bookRepository.GetBookByIdAsync(bookId);
            if (book != null)
            {
                book.ReadingStatus = ReadingStatus.Started;
                await bookRepository.UpdateBookAsync(book);
            }
        }

        public async Task EndReadingAsync(int bookId)
        {
            var book = await bookRepository.GetBookByIdAsync(bookId);
            if (book != null)
            {
                book.ReadingStatus = ReadingStatus.Ended;
                await bookRepository.UpdateBookAsync(book);
            }
        }
    }
}