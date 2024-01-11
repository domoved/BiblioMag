using System;
using System.Threading.Tasks;

namespace BiblioMag.Models.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IBookRepository BookRepository;

        public DownloadService(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            try
            {
                return await BookRepository.GetBookByIdAsync(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get book by id: " + ex.Message);
            }
        }
    }
}