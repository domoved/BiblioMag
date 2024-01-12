using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BiblioMag.Models.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IBookRepository BookRepository;

        private static LibraryDbContext DbContext;

        public DownloadService(IBookRepository bookRepository, LibraryDbContext dbContext)
        {
            DbContext = dbContext;
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
        public static async Task<byte[]?> DownloadBookAsync(int bookId)
        {
            try
            {
                var book = await DbContext.Books.FindAsync(bookId);
                return book?.FileContent;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to download book: " + ex.Message);
            }
        }
    }
}