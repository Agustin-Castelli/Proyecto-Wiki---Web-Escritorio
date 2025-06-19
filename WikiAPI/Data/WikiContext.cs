using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WikiAPI.Models.Entities;

namespace WikiAPI.Data
{
    public class WikiContext : DbContext
    {
        public WikiContext(DbContextOptions<WikiContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Keyword> Keywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración relación N a N
            modelBuilder.Entity<WikiArticleKeyword>()
                .HasKey(wak => new { wak.ArticleId, wak.KeywordId });

            modelBuilder.Entity<WikiArticleKeyword>()
                .HasOne(wak => wak.Article)
                .WithMany(a => a.Keywords)
                .HasForeignKey(wak => wak.ArticleId);

            modelBuilder.Entity<WikiArticleKeyword>()
                .HasOne(wak => wak.Keyword)
                .WithMany(k => k.Articles)
                .HasForeignKey(wak => wak.KeywordId);

            // Configuración de tipos de datos para CAPSOL
            modelBuilder.Entity<Article>()
                .Property(a => a.Title)
                .HasColumnType("varchar(50)");

            modelBuilder.Entity<Keyword>()
                .Property(k => k.Name)
                .HasColumnType("varchar(50)");
        }
    }
}
