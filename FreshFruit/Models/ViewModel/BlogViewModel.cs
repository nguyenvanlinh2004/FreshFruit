namespace FreshFruit.Models.ViewModel
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Contents { get; set; }
        public string? Image { get; set; }
        public int AuthorId { get; set; }
        public int Status { get; set; } = 1;
    }
}
