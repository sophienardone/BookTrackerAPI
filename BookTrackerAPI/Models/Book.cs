using System.ComponentModel.DataAnnotations;

namespace BookTrackerAPI.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PagesRead { get; set; }
        public List<UserProgress> UserProgress  { get; set; }
        public List <Review> Review { get; set; }

    }
}
