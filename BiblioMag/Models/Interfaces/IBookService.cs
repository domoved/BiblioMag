namespace BiblioMag.Models.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Book> AddBookAsync(Book book);
        Task<bool> RemoveBookAsync(int bookId);
        Task<Book> UpdateBookAsync(Book book);
        Task<byte[]?> DownloadBookAsync(int bookId);
    }
}