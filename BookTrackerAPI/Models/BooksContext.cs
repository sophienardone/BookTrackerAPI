using Microsoft.EntityFrameworkCore;

namespace BookTrackerAPI.Models
{
    public class BooksContext : DbContext
    {
        public string DbPath { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<UserProgress> UserProgresses { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
             new User
             {
                 Id = 1,
                 Username = "Saoirse McGuinness",
                 Email = "Saoirsemcg@yahoo.ie"
             }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Call me By your Name",
                    Description = "",
                    Author = "Andre Aciman",
                    Genre = "Romance",
                    PagesRead = 1,
                }
                );

            modelBuilder.Entity<UserProgress>().HasData(
                new UserProgress
                {
                    Id = 1,
                    BookId = 1,
                    UserId = 1,
                    PagesRead = 100,
                    LastUpdated = DateTime.Now
                }
                );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    UserId = 1,
                    BookId = 1,
                    Rating = 5,
                    Comment = "The novel centers on the sudden and powerful romance between Elio and Oliver"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
