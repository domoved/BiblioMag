using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BiblioMag.Models
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<ReadingSession> ReadingSessions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка правильного отображения модели на таблицу в базе данных
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(x => x.FileContent)
                    .HasColumnType("bytea"); // Для PostgreSQL нужно явно указать тип данных для хранения бинарных данных
            });
        }
    }
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=library;Username=postgres;Password=xdd");

            return new LibraryDbContext(optionsBuilder.Options);
        }
    }
}