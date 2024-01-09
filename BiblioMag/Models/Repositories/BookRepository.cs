using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioMag.Models;
using BiblioMag.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace BiblioMag.Models.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await dbContext.Books.ToListAsync();
        }

        public async Task<bool> RemoveBookAsync(int bookId)
        {
            var book = await dbContext.Books.FindAsync(bookId);
            if (book != null)
            {
                dbContext.Books.Remove(book);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return book;
        }
    }
}