namespace BiblioMag.Models.Services
{
    public interface IReadingService
    {
        Task StartReadingAsync(int bookId);
        Task EndReadingAsync(int bookId);
    }
}