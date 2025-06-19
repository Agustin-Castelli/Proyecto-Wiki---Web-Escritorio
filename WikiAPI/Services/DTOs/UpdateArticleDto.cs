namespace WikiAPI.Services.DTOs
{
    public class UpdateArticleDto
    {
        public int Id { get; set; } // QUITAR ID PARA EL UPDATE, EL MISMO VIAJA POR URL Y ES UN ERROR TENERLO AQUI
        public string Title { get; set; }
        public string? Description { get; set; }
        public string CreatedByUser { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new();
    }
}