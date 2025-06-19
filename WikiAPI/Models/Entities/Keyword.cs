using System.ComponentModel.DataAnnotations.Schema;

namespace WikiAPI.Models.Entities
{
    public class Keyword
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;

        // Relación N a N
        public ICollection<WikiArticleKeyword> Articles { get; set; }
    }
}
