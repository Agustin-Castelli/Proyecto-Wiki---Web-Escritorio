using WikiAPI.Data.Interfaces;
using WikiAPI.Models.Entities;
using WikiAPI.Services.DTOs;
using WikiAPI.Services.Interfaces;

namespace WikiAPI.Services
{
    public class KeywordService : IKeywordService
    {
        private readonly IKeywordRepository _repo;
        private readonly ILogger<KeywordService> _logger;

        public KeywordService(IKeywordRepository repo, ILogger<KeywordService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<UpdateKeywordDto>> GetAllAsync()
        {
            try
            {
                var keywords = await _repo.GetAllAsync();
                return keywords.Select(k => new UpdateKeywordDto
                {
                    Id = k.Id,
                    Name = k.Name,
                    Description = k.Description
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las keywords");
                throw;
            }
        }

        public async Task<UpdateKeywordDto?> GetByIdAsync(int id)
        {
            try
            {
                var keyword = await _repo.GetByIdAsync(id);
                return keyword == null ? null : new UpdateKeywordDto
                {
                    Id = keyword.Id,
                    Name = keyword.Name,
                    Description = keyword.Description
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener keyword con ID {id}");
                throw;
            }
        }

        public async Task<List<string>> GetByNameAsync(string name)
        {
            try
            {
                var keywords = await _repo.SearchByNameAsync(name);
                return keywords.Select(k => k.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al buscar keywords con nombre que contenga '{name}'");
                throw;
            }
        }

        public async Task AddAsync(CreateKeywordDto dto)
        {
            try
            {
                var existing = await _repo.GetByNameAsync(dto.Name);
                if (existing != null)
                    throw new Exception("Keyword already exists.");

                var keyword = new Keyword
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                await _repo.AddAsync(keyword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al agregar keyword {dto.Name}");
                throw;
            }
        }

        public async Task UpdateAsync(UpdateKeywordDto dto)
        {
            try
            {
                var keyword = await _repo.GetByIdAsync(dto.Id);
                if (keyword == null) return;

                keyword.Name = dto.Name;
                keyword.Description = dto.Description;

                await _repo.UpdateAsync(keyword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar keyword ID {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar keyword ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<UpdateKeywordDto>> SearchAsync(string query)
        {
            try
            {
                var keywords = await _repo.SearchAsync(query);
                return keywords.Select(k => new UpdateKeywordDto
                {
                    Id = k.Id,
                    Name = k.Name,
                    Description = k.Description
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al buscar keywords con query '{query}'");
                throw;
            }
        }
    }


}
