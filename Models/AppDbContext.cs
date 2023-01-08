using Microsoft.EntityFrameworkCore;

namespace wykład_4.Models
{
    public class AppDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        private string DbPath;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "books.db");
            optionsBuilder.UseSqlite($"DATA SOURCE={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                 .HasData(
                new Book() { Id= 1, Title = "ASP.NET", EditionYear=2020},
                new Book() { Id = 2, Title = "C#", EditionYear = 2022 },
                new Book() { Id = 3, Title = "Java", EditionYear = 2021 }
                );
        }
    }
}
