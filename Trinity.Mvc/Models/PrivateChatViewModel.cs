namespace Trinity.Mvc.Models
{
    public class PrivateChatViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
    }
}