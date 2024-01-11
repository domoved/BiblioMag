using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiblioMag.Models.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext DbContext;

        public BookService(LibraryDbContext dbContext)
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
                return await DbContext.Books.FindAsync(bookId);
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

        public async Task<byte[]?> DownloadBookAsync(int bookId)
        {
            try
            {
                var book = await DbContext.Books.FindAsync(bookId);
                if (book != null)
                {
                    return book.FileContent;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to download book: " + ex.Message);
            }
        }

        public async Task<ReadingSession> AddReadingSessionAsync(ReadingSession session)
        {
            try
            {
                DbContext.ReadingSessions.Add(session);
                await DbContext.SaveChangesAsync();
                return session;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add reading session: " + ex.Message);
            }
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            try
            {
                DbContext.Entry(book).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book: " + ex.Message);
            }
        }
    }
}