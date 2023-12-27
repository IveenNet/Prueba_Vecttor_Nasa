using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba_Vecttor_Nasa.Models;
using Prueba_Vecttor_Nasa.Services;

namespace Prueba_Vecttor_Nasa.Controllers
{
    [ApiController]
    [Route("asteroids")]
    public class AsteroidsController : ControllerBase
    {
        private readonly INasaService _nasaService;

        public AsteroidsController(INasaService nasaService)
        {
            _nasaService = nasaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? days)
        {
            if (!days.HasValue)
            {
                // Devuelve un error controlado si 'days' no se proporciona
                return BadRequest("El parámetro 'days' es obligatorio.");
            }
            else if (days < 1 || days > 7)
            {
                // Devuelve un error si 'days' está fuera del rango permitido
                return BadRequest("El parámetro 'days' debe estar en el rango de 1 a 7.");
            }

            try
            {
                var asteroids = await _nasaService.GetAsteroidsAsync(days.Value);
                return Ok(asteroids);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError // O un código de estado más apropiado
                };
                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }
    }
}
