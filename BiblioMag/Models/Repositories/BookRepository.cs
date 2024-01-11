using BiblioMag.Models;
using BiblioMag.Models.Services;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext DbContext;

    public BookRepository(LibraryDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        try
        {
            DbContext.Books.Add(book);
            await DbContext.SaveChangesAsync();
            return book;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to add book: " + ex.Message);
        }
    }

    public async Task<Book> GetBookByIdAsync(int bookId)
    {
        try
        {
            return await DbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to get book by id: " + ex.Message);
        }
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        try
        {
            return await DbContext.Books.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to get all books: " + ex.Message);
        }
    }

    public async Task<bool> RemoveBookAsync(int bookId)
    {
        try
        {
            var book = await DbContext.Books.FindAsync(bookId);
            if (book != null)
            {
                DbContext.Books.Remove(book);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to remove book: " + ex.Message);
        }
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        try
        {
            var existingBook = await DbContext.Books.FindAsync(book.Id);
            if (existingBook != null)
            {
                DbContext.Entry(existingBook).CurrentValues.SetValues(book);
                await DbContext.SaveChangesAsync();
                return existingBook;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to update book: " + ex.Message);
        }
    }
}