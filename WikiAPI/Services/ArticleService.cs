using WikiAPI.Data.Interfaces;
using WikiAPI.Models.Entities;
using WikiAPI.Services.DTOs;
using WikiAPI.Services.Interfaces;

namespace WikiAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repo;
        private readonly IKeywordRepository _keywordRepo;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(IArticleRepository repo, IKeywordRepository keywordRepo, ILogger<ArticleService> logger)
        {
            _repo = repo;
            _keywordRepo = keywordRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<UpdateArticleDto>> GetAllAsync()
        {
            try
            {
                var articles = await _repo.GetAllAsync();
                return articles.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAllAsync.");
                throw;
            }
        }

        public async Task<UpdateArticleDto?> GetByIdAsync(int id)
        {
            try
            {
                var article = await _repo.GetByIdAsync(id);
                return article is null ? null : MapToDto(article);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en GetByIdAsync con ID {id}.");
                throw;
            }
        }

        public async Task AddAsync(CreateArticleDto dto)
        {
            try
            {
                var existingKeywords = await _keywordRepo.GetExistingKeywordsAsync(dto.Keywords);
                var existingNames = existingKeywords.Select(k => k.Name.ToLower()).ToHashSet();

                var newKeywords = dto.Keywords
                    .Where(k => !existingNames.Contains(k.ToLower()))
                    .Select(k => new Keyword { Name = k })
                    .ToList();

                var allKeywords = existingKeywords.Concat(newKeywords).ToList();

                var article = new Article
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    CreatedByUser = dto.CreatedByUser,
                    Keywords = allKeywords.Select(k => new WikiArticleKeyword { Keyword = k }).ToList()
                };

                await _repo.AddAsync(article);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en AddAsync.");
                throw;
            }
        }

        public async Task UpdateAsync(UpdateArticleDto dto)
        {
            try 
            {
                var existing = await _repo.GetByIdAsync(dto.Id);
                if (existing == null) return;

                existing.Title = dto.Title;
                existing.Description = dto.Description;
                existing.CreatedByUser = dto.CreatedByUser;
                existing.UpdateDate = DateTime.UtcNow;

                // Obtener keywords existentes
                var existingKeywords = await _keywordRepo.GetExistingKeywordsAsync(dto.Keywords);
                var existingNames = existingKeywords.Select(k => k.Name.ToLower()).ToHashSet();

                // Crear nuevas keywords que no existen
                var newKeywords = dto.Keywords
                    .Where(k => !existingNames.Contains(k.ToLower()))
                    .Select(k => new Keyword { Name = k })
                    .ToList();

                var allKeywords = existingKeywords.Concat(newKeywords).ToList();

                // Limpiar relaciones anteriores y asignar nuevas
                existing.Keywords.Clear();
                existing.Keywords = allKeywords
                    .Select(k => new WikiArticleKeyword { ArticleId = existing.Id, Keyword = k })
                    .ToList();

                await _repo.UpdateAsync(existing);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Error en UpdateAsync con ID {dto.Id}.");
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
                _logger.LogError(ex, $"Error en DeleteAsync con ID {id}.");
                throw;
            }
        }

        public async Task<IEnumerable<UpdateArticleDto>> SearchAsync(string query)
        {
            try
            {
                var results = await _repo.SearchAsync(query);
                return results.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en SearchAsync con query '{query}'.");
                throw;
            }
        }

        private static UpdateArticleDto MapToDto(Article article) => new()
        {
            Id = article.Id,
            Title = article.Title,
            Description = article.Description,
            CreatedByUser = article.CreatedByUser,
            Keywords = article.Keywords.Select(k => k.Keyword.Name).ToList()
        };
    }
}
