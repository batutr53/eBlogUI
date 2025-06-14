namespace eBlog.Application.DTOs.Dashboard
{
    public class PersonalSummaryDto
    {
        public int TotalPosts { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public string LastActive { get; set; } = null!;
    }
}
