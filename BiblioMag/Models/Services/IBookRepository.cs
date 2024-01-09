namespace BiblioMag.Models.Services
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<bool> RemoveBookAsync(int bookId);
        Task<Book> UpdateBookAsync(Book book);
    }
}