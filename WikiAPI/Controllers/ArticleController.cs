using Microsoft.AspNetCore.Mvc;
using WikiAPI.Services.DTOs;
using WikiAPI.Services.Interfaces;

namespace WikiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _service;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IArticleService service, ILogger<ArticleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var articles = await _service.GetAllAsync();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los artículos.");
                throw;
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var article = await _service.GetByIdAsync(id);
                return article == null ? NotFound() : Ok(article);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el artículo con ID {id}.");
                throw;
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateArticleDto dto)
        {
            try
            {
                await _service.AddAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un artículo.");
                throw;
            }
        }

        // Sí, ya sé que no es RESTful. Después lo cambio, ok?
        [HttpPut("[action]")] 
        public async Task<IActionResult> Update([FromBody] UpdateArticleDto dto) // Tendríamos que modificar esto para que le pase el ID por parámetro y en el service validar que id == dto.Id
        {
            try
            {
                await _service.UpdateAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el artículo con ID {dto.Id}.");
                throw;
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el artículo con ID {id}.");
                throw;
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            try
            {
                var result = await _service.SearchAsync(q);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al buscar artículos con query '{q}'.");
                throw;
            }
        }
    }
}