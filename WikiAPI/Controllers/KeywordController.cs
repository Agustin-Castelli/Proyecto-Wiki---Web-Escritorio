using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WikiAPI.Services.DTOs;
using WikiAPI.Services.Interfaces;

namespace WikiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordController : ControllerBase
    {
        private readonly IKeywordService _service;
        private readonly ILogger<KeywordController> _logger;

        public KeywordController(IKeywordService service, ILogger<KeywordController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAll()");
                return StatusCode(500, "Ocurrió un error al obtener las keywords.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en GetById (id={id})");
                return StatusCode(500, "Ocurrió un error al obtener la keyword.");
            }
        }

        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var result = await _service.GetByNameAsync(name);
                return Ok(result); // aunque venga vacío, está bien
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en GetByName (name={name})");
                return StatusCode(500, "Ocurrió un error al buscar las keywords.");
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            try
            {
                var result = await _service.SearchAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en Search (query={query})");
                return StatusCode(500, "Ocurrió un error en la búsqueda.");
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateKeywordDto dto)
        {
            try
            {
                await _service.AddAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Create");
                return BadRequest(ex.Message);
            }
        }

        // Sí, ya sé que no es RESTful. Después lo cambio, ok?
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateKeywordDto dto)  // Tendríamos que modificar esto para que le pase el ID por parámetro y en el service validar que id == dto.Id
        {
            try
            {
                await _service.UpdateAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Update");
                return StatusCode(500, "Ocurrió un error al actualizar.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en Delete (id={id})");
                return StatusCode(500, "Ocurrió un error al eliminar.");
            }
        }
    }
}
