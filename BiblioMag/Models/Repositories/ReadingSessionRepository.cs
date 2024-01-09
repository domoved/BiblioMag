using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioMag.Models;
using BiblioMag.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace BiblioMag.Models.Repositories
{
    public class ReadingSessionRepository : IReadingSessionRepository
    {
        private readonly LibraryDbContext dbContext;

        public ReadingSessionRepository(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ReadingSession> AddReadingSessionAsync(ReadingSession session)
        {
            dbContext.ReadingSessions.Add(session);
            await dbContext.SaveChangesAsync();
            return session;
        }
    }
}