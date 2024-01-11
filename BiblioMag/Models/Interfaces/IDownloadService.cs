namespace BiblioMag.Models.Services
{
    public interface IDownloadService
    {
        Task<Book> GetBookByIdAsync(int bookId);
    }
}