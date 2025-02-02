namespace BookTrackerAPI.Controllers
{
    public class UserProgressDTO
    {
        public int BookId { get; set; }
        public int PagesRead { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
