using System;
using System.Threading.Tasks;

namespace BiblioMag.Models.Services
{
    public class ReadingService : IReadingService
    {
        private readonly IBookRepository BookRepository;

        public ReadingService(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        public async Task StartReadingAsync(int bookId)
        {
            try
            {
                var book = await BookRepository.GetBookByIdAsync(bookId);
                if (book != null)
                {
                    book.ReadingStatus = ReadingStatus.Started;
                    await BookRepository.UpdateBookAsync(book);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to start reading: " + ex.Message);
            }
        }

        public async Task EndReadingAsync(int bookId)
        {
            try
            {
                var book = await BookRepository.GetBookByIdAsync(bookId);
                if (book != null)
                {
                    book.ReadingStatus = ReadingStatus.Ended;
                    await BookRepository.UpdateBookAsync(book);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to end reading: " + ex.Message);
            }
        }
    }
}