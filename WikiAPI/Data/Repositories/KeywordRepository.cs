using Microsoft.EntityFrameworkCore;
using WikiAPI.Data.Interfaces;
using WikiAPI.Models.Entities;

namespace WikiAPI.Data.Repositories
{
    public class KeywordRepository : IKeywordRepository
    {
        private readonly WikiContext _context;

        public KeywordRepository(WikiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Keyword>> GetAllAsync()
        {
            return await _context.Keywords.ToListAsync();
        }

        public async Task<Keyword?> GetByIdAsync(int id)
        {
            return await _context.Keywords.FindAsync(id);
        }

        public async Task<Keyword?> GetByNameAsync(string name)
        {
            return await _context.Keywords
                .FirstOrDefaultAsync(k => k.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Keyword>> SearchByNameAsync(string name)
        {
            return await _context.Keywords
                .Where(k => k.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Keyword>> GetExistingKeywordsAsync(IEnumerable<string> names)
        {
            var lowerNames = names.Select(n => n.ToLower()).ToList();
            return await _context.Keywords
                .Where(k => lowerNames.Contains(k.Name.ToLower()))
                .ToListAsync();
        }

        public async Task AddAsync(Keyword keyword)
        {
            _context.Keywords.Add(keyword);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Keyword keyword)
        {
            _context.Keywords.Update(keyword);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var keyword = await _context.Keywords.FindAsync(id);
            if (keyword != null)
            {
                _context.Keywords.Remove(keyword);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Keyword>> SearchAsync(string query)
        {
            return await _context.Keywords
                .Where(k => k.Name.Contains(query))
                .ToListAsync();
        }
    }

}
