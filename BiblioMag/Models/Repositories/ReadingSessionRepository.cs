using BiblioMag.Models.Services;

namespace BiblioMag.Models.Repositories
{
    public class ReadingSessionRepository : IReadingSessionRepository
    {
        private readonly LibraryDbContext DbContext;

        public ReadingSessionRepository(LibraryDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<ReadingSession> AddReadingSessionAsync(ReadingSession session)
        {
            try
            {
                DbContext.ReadingSessions.Add(session);
                await DbContext.SaveChangesAsync();
                return session;
            }
            catch
            {
                throw new Exception("Failed to add reading session");
            }
        }
    }
}