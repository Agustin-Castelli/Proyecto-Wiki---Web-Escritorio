using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiShared.DTOs.Article;
using WikiShared.DTOs.Keyword;
using WikiShared.Responses;

namespace WikiShared.Interfaces
{
    public interface IKeywordService
    {
        Task<List<KeywordDto>> GetAllAsync();
        Task<KeywordDto?> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(CreateKeywordDto dto);
        Task<ApiResponse> UpdateAsync(UpdateKeywordDto dto);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
