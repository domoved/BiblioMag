using Microsoft.EntityFrameworkCore;

namespace BiblioMag.Models.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IBookRepository bookRepository;

        public DownloadService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await bookRepository.GetBookByIdAsync(bookId);
        }
    }
}