namespace WikiAPI.Models.Entities
{
    public class WikiArticleKeyword
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
