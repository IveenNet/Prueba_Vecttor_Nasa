using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba_Vecttor_Nasa.Services;

namespace Prueba_Vecttor_Nasa.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AsteroidsController : ControllerBase
	{
		private readonly INasaService _nasaService;

		public AsteroidsController(INasaService nasaService)
		{
			_nasaService = nasaService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(int numberOfDays)
		{
			try
			{
				var asteroids = await _nasaService.GetAsteroidsAsync(numberOfDays);
				return Ok(asteroids);
			}
			catch (Exception ex)
			{
				// Manejar la excepción adecuadamente
				return StatusCode(500, ex.Message);
			}
		}
	}
}
