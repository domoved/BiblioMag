using Microsoft.EntityFrameworkCore;

namespace BiblioMag.Models
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<ReadingSession> ReadingSessions { get; set; } = null!;
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }
    }
}