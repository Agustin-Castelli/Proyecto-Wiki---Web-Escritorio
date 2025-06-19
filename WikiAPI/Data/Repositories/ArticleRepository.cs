using Microsoft.EntityFrameworkCore;
using WikiAPI.Data.Interfaces;
using WikiAPI.Models.Entities;

namespace WikiAPI.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly WikiContext _context;

        public ArticleRepository(WikiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles.Include(a => a.Keywords).ThenInclude(k => k.Keyword).ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)   // CHEQUEAR ESTE METODO A VER SI FUNCIONA BIEN
        {
            return await _context.Articles.Include(a => a.Keywords).ThenInclude(k => k.Keyword)
                                          .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await GetByIdAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Article>> SearchAsync(string? query)
        {
            return await _context.Articles
                .Include(a => a.Keywords).ThenInclude(k => k.Keyword)
                .Where(a =>
                    a.Title.Contains(query!) ||
                    a.Description.Contains(query!) ||
                    a.CreatedByUser.Contains(query!) ||
                    a.Keywords.Any(k =>
                        k.Keyword.Name.Contains(query!) ||
                        k.Keyword.Description.Contains(query!)  // <- Esto faltaba
                    )
                )
                .ToListAsync();
        }
    }
}
