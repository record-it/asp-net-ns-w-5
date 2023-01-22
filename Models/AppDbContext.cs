using Microsoft.EntityFrameworkCore;

namespace wykład_4.Models
{
    public class AppDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> BookDetailsSet { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Author> Authors { get; set; }

        private string DbPath;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "books-v1.db");
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

            modelBuilder.Entity<BookDetails>()
                .HasData(
                    new BookDetails() { Id = 1, Description = "Super", BookId = 1}
                );
            modelBuilder.Entity<Author>()
                .HasData(
                new Author() { Id = 1, Name= "John"},
                new Author() { Id = 2, Name = "Adam" }
                );
            modelBuilder.Entity<Book>()
                .HasMany<Author>(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(join => join.HasData(
                    new { BooksId = 1, AuthorsId = 1 },
                    new { BooksId = 1, AuthorsId = 2 },
                    new { BooksId = 2, AuthorsId = 2 }
                    ));

        }
    }
}
