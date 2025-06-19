using WikiAPI.Models.Entities;

namespace WikiAPI.Data.Interfaces
{
    public interface IKeywordRepository
    {
        Task<IEnumerable<Keyword>> GetAllAsync();
        Task<Keyword?> GetByIdAsync(int id);
        Task<Keyword?> GetByNameAsync(string name);
        Task<List<Keyword>> SearchByNameAsync(string name);
        Task<List<Keyword>> GetExistingKeywordsAsync(IEnumerable<string> names);
        Task AddAsync(Keyword keyword);
        Task UpdateAsync(Keyword keyword);
        Task DeleteAsync(int id);
        Task<IEnumerable<Keyword>> SearchAsync(string query);
    }
}
