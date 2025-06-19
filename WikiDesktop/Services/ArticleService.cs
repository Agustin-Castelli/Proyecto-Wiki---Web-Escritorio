using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WikiShared.DTOs.Article;
using WikiShared.Interfaces;
using WikiShared.Responses;

namespace WikiDesktop.Services
{
    public class ArticleService : IArticleService
    {
        private readonly HttpClient _http;

        public ArticleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ArticleDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ArticleDto>>("api/Article/GetAll") ?? new();
        }

        public async Task<ArticleDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ArticleDto>($"api/Article/GetById/{id}");
        }

        public async Task<List<ArticleDto>> SearchAsync(string query)
        {
            var response = await _http.GetAsync($"api/Article/Search?q={Uri.EscapeDataString(query)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ArticleDto>>() ?? new();
            }

            return new();
        }

        public async Task<ApiResponse> CreateAsync(CreateArticleDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Article/Create", dto);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse { Success = true, Message = "Article creado con éxito." };
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            return new ApiResponse { Success = false, Message = errorMsg };
        }

        public async Task<ApiResponse> UpdateAsync(UpdateArticleDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/Article/Update", dto);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse { Success = true, Message = "Article actualizado con éxito." };
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            return new ApiResponse { Success = false, Message = errorMsg };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Article/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse { Success = true, Message = "Article eliminado con éxito." };
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            return new ApiResponse { Success = false, Message = errorMsg };
        }
    }
}
