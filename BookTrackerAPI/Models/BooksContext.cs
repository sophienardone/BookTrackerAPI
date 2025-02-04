using Microsoft.EntityFrameworkCore;
using System.Net;

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
                 Username = "Saoirsemcg",
                 Email = "Saoirsemcg@yahoo.ie"
             },
             new User
             {
                 Id = 2,
                 Username = "Shaunac",
                 Email = "ShaunaCooney@gmail.com"
             },
             new User
             {
                 Id = 3,
                 Username = "Sophienardone",
                 Email = "Sophien25@gmail.com"
             }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Call me By your Name",
                    Description = "The novel centers on the sudden and powerful romance that blossoms between Elio and Oliver",
                    Author = "Andre Aciman",
                    Genre = "Romance",
                    PagesRead = 1,
                },
                new Book
                {
                    BookId = 2,
                    Title = "Normal People",
                    Description = "The novel follows the complex friendship and relationship between two teenagers from different social classes, Connell and Marianne",
                    Author = "Sally Rooney",
                    Genre = "Romance",
                    PagesRead = 150,
                },
                new Book
                {
                    BookId = 3,
                    Title = "Poor",
                    Description = "Her candid book exposes the harsh realities of growing up in a household plagued by addiction, abuse, and financial hardship",
                    Author = "Katriona O'Sullivan",
                    Genre = "Autobiography",
                    PagesRead = 300,
                },
                new Book
                {
                    BookId = 4,
                    Title = "The Fault in Our Stars",
                    Description = "The story is narrated by Hazel Grace Lancaster, a 16-year-old girl with thyroid cancer.",
                    Author = "John Greene",
                    Genre = "Young adult",
                    PagesRead = 204
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
                },
                new UserProgress 
                {
                    Id=2,
                    BookId = 2,
                    UserId = 2,
                    PagesRead= 46,
                    LastUpdated = DateTime.Now
                },
                new UserProgress
                {
                    Id=3,
                    BookId = 3,
                    UserId = 3,
                    PagesRead= 57,
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
                    Comment = "Great book. Couldn't put it down!"
                },
                new Review
                {
                    Id = 2,
                    UserId = 2,
                    BookId = 2,
                    Rating = 4,
                    Comment = "Fantastic Book."
                },
                new Review
                { 
                Id = 3,
                    UserId = 3,
                    BookId = 3,
                    Rating = 5,
                    Comment = "Even better than the film"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
