using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiShared.DTOs.Article;
using WikiShared.Responses;

namespace WikiShared.Interfaces
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllAsync();
        Task<ArticleDto?> GetByIdAsync(int id);
        Task<List<ArticleDto>> SearchAsync(string query);
        Task<ApiResponse> CreateAsync(CreateArticleDto dto);
        Task<ApiResponse> UpdateAsync(UpdateArticleDto dto);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
