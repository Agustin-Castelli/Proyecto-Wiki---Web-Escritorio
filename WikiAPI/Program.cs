using Microsoft.EntityFrameworkCore;
using WikiAPI.Data;
using WikiAPI.Data.Interfaces;
using WikiAPI.Data.Repositories;
using WikiAPI.Services.Interfaces;
using WikiAPI.Services;
using WikiAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Database
builder.Services.AddDbContext<WikiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Repositories
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IKeywordRepository, KeywordRepository>();
#endregion

#region Services
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IKeywordService, KeywordService>();
#endregion


var app = builder.Build();

// Usar CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
