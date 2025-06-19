namespace WikiAPI.Services.DTOs
{
    public class CreateArticleDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedByUser { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new();
    }
}
