namespace BiblioMag.Models.Services
{
    public interface IReadingSessionRepository
    {
        Task<ReadingSession> AddReadingSessionAsync(ReadingSession session);
    }
}