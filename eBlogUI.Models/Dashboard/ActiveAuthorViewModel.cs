namespace eBlogUI.Models.Dashboard
{
    public class ActiveAuthorViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int PostCount { get; set; }
        public int TotalLikes { get; set; }
    }
}
