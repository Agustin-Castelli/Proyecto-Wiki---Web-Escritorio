using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiShared.DTOs.Keyword;

namespace WikiShared.DTOs.Article
{
    public class UpdateArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedByUser { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new();
    }
}
