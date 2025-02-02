namespace BookTrackerAPI.Controllers
{
    public class UserDTO
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserProgressDTO>? UserProgress { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }
    }
}
