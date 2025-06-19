using WikiShared.DTOs.Keyword;
using WikiShared.Interfaces;
using WikiShared.Responses;
using System.Net.Http.Json;

namespace WikiDesktop.Services
{
    public class KeywordService : IKeywordService
    {
        private readonly HttpClient _http;

        public KeywordService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<KeywordDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<KeywordDto>>("api/Keyword/GetAll") ?? new();
        }

        public async Task<KeywordDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<KeywordDto>($"api/Keyword/GetById/{id}");
        }

        public async Task<ApiResponse> CreateAsync(CreateKeywordDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Keyword/Create", dto);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse { Success = true, Message = "Keyword creada con éxito." };
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            return new ApiResponse { Success = false, Message = errorMsg };
        }

        public async Task<ApiResponse> UpdateAsync(UpdateKeywordDto dto)
        {
            var response = await _http.PutAsJsonAsync("api/Keyword/Update", dto);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse { Success = true, Message = "Keyword actualizada con éxito." };
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            return new ApiResponse { Success = false, Message = errorMsg };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Keyword/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse { Success = true, Message = "Keyword eliminada con éxito." };
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            return new ApiResponse { Success = false, Message = errorMsg };
        }
    }
}
