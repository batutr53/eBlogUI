namespace eBlog.Domain.Models
{
    public class PersonalSummary
    {
        public int TotalPosts { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public DateTime LastActive { get; set; }
    }
}
