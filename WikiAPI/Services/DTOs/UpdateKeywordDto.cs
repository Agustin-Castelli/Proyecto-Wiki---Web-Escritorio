namespace WikiAPI.Services.DTOs
{
    public class UpdateKeywordDto
    {
        public int Id { get; set; } // QUITAR ID PARA EL UPDATE, EL MISMO VIAJA POR URL Y ES UN ERROR TENERLO AQUI
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
