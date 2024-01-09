using Microsoft.EntityFrameworkCore;

namespace BiblioMag.Models.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            context.Add(book);
            await context.SaveChangesAsync();
            return book;
        }

        public async Task<ReadingSession> AddReadingSessionAsync(ReadingSession session)
        {
            context.Add(session);
            await context.SaveChangesAsync();
            return session;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await context.Books.FindAsync(bookId);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await context.Books.ToListAsync();
        }

        public async Task<bool> RemoveBookAsync(int bookId)
        {
            var book = await context.Books.FindAsync(bookId);
            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return book;
        }

        public async Task StartReadingAsync(int bookId)
        {
            var readingSession = new ReadingSession
            {
                BookId = bookId,
                StartTime = DateTime.Now
            };

            context.Add(readingSession);
            await context.SaveChangesAsync();
        }

        public async Task EndReadingAsync(int bookId)
        {
            var readingSession = new ReadingSession
            {
                BookId = bookId,
                StartTime = DateTime.Now
            };

            context.Add(readingSession);
            await context.SaveChangesAsync();
        }

        public async Task<byte[]?> DownloadBookAsync(int bookId)
        {
            var book = await context.Books.FindAsync(bookId);
            if (book != null)
            {
                return book.FileContent;
            }
            return null;
        }
    }
}