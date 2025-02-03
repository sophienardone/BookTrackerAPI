namespace BookTrackerAPI.Controllers
{
    public class UserProgressDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int PagesRead { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }

    }
}
