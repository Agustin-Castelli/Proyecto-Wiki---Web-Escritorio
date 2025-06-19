using WikiAPI.Services.DTOs;

namespace WikiAPI.Services.Interfaces
{
    public interface IKeywordService
    {
        Task<IEnumerable<UpdateKeywordDto>> GetAllAsync();
        Task<UpdateKeywordDto?> GetByIdAsync(int id);
        Task<List<string>> GetByNameAsync(string name);
        Task AddAsync(CreateKeywordDto dto);
        Task UpdateAsync(UpdateKeywordDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<UpdateKeywordDto>> SearchAsync(string query);
    }
}
