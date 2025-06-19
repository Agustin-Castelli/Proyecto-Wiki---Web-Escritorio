using WikiAPI.Models.Entities;

namespace WikiAPI.Data.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task AddAsync(Article article);
        Task UpdateAsync(Article article);
        Task DeleteAsync(int id);
        Task<IEnumerable<Article>> SearchAsync(string? query);
    }
}

