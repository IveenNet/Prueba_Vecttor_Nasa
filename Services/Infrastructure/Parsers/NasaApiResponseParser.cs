using Newtonsoft.Json;
using Prueba_Vecttor_Nasa.Models.APIModels;
using Prueba_Vecttor_Nasa.Models.DTOs;
using Prueba_Vecttor_Nasa.Models.Responses;

namespace Prueba_Vecttor_Nasa.Services.Infrastructure.Parsers
{
	public class NasaApiResponseParser
	{
		public IEnumerable<AsteroidModel> ParseAsteroidResponse(string jsonResponse)
		{
			if (string.IsNullOrEmpty(jsonResponse))
			{
				throw new ArgumentException("La respuesta JSON no puede ser nula o vacía.");
			}

			var asteroidData = JsonConvert.DeserializeObject<AsteroidResponse>(jsonResponse)
							   ?? throw new InvalidOperationException("Los datos de Near Earth Objects no están disponibles en la respuesta JSON.");

			return asteroidData.NearEarthObjects?
				.AsParallel() // Procesamiento paralelo
				.SelectMany(neo => neo.Value)
				.Where(a => a.IsPotentiallyHazardousAsteroid)
				.Select(a => CreateAsteroidModel(a))
				.OrderByDescending(a => a.Diameter)
				.Take(3)
				.ToList() // Para forzar la evaluación y aprovechar el procesamiento paralelo
				?? Enumerable.Empty<AsteroidModel>();
		}


		private AsteroidModel CreateAsteroidModel(NearEarthObject neo)
		{
			var firstApproachData = neo.CloseApproachData.FirstOrDefault();
			return new AsteroidModel
			{
				Name = neo.Name,
				Diameter = CalculateAverageDiameter(neo.EstimatedDiameter.Kilometers),
				Velocity = firstApproachData?.RelativeVelocity.KilometersPerHour,
				Date = firstApproachData?.CloseApproachDate,
				Planet = firstApproachData?.OrbitingBody.ToString() // Manejo de posible valor nulo
			};
		}

		private double CalculateAverageDiameter(Feet diameter)
		{
			return (diameter.EstimatedDiameterMin + diameter.EstimatedDiameterMax) / 2;
		}
	}
}
