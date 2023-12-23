using System.Collections.Generic;
using System.Threading.Tasks;
using Prueba_Vecttor_Nasa.Models;

namespace Prueba_Vecttor_Nasa.Services
{
	public interface INasaService
	{
		Task<IEnumerable<NearEarthObject>> GetAsteroidsAsync(int numberOfDays);
	}
}
