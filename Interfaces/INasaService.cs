using Prueba_Vecttor_Nasa.Models.DTOs;

namespace Prueba_Vecttor_Nasa.Interfaces
{
	public interface INasaService
	{
		Task<IEnumerable<AsteroidModel>> GetAsteroidsAsync(int days);
	}
}
