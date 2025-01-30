namespace BookTrackerAPI.Models
{
    public class UserProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int PagesRead {  get; set; }
        public DateTime LastUpdated { get; set; }

        public User User { get; set; }
        public Book book { get; set; }

    }
}
