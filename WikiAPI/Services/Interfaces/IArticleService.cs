using WikiAPI.Services.DTOs;

namespace WikiAPI.Services.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<UpdateArticleDto>> GetAllAsync();
        Task<UpdateArticleDto?> GetByIdAsync(int id);
        Task AddAsync(CreateArticleDto dto);
        Task UpdateAsync(UpdateArticleDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<UpdateArticleDto>> SearchAsync(string query);
    }
}
