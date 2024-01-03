using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Prueba_Vecttor_Nasa.Models.APIModels;
using Prueba_Vecttor_Nasa.Models.DTOs;
using Prueba_Vecttor_Nasa.Models.Responses;

namespace Prueba_Vecttor_Nasa.Services.Infrastructure.Parsers
{
	public class NasaApiResponseParser
	{

		private readonly IMemoryCache _cache;
		private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

		public NasaApiResponseParser(IMemoryCache cache)
		{
			_cache = cache;
		}

		public IEnumerable<AsteroidModel> ParseAsteroidResponse(string jsonResponse)
		{
			if (string.IsNullOrEmpty(jsonResponse))
			{
				return Enumerable.Empty<AsteroidModel>();
			}



			// Intenta obtener el valor del caché
			if (_cache.TryGetValue(jsonResponse, out IEnumerable<AsteroidModel> cachedResult))
			{
				return cachedResult;
			}

			var asteroidData = JsonConvert.DeserializeObject<AsteroidResponse>(jsonResponse)
							   ?? throw new InvalidOperationException("Los datos de Near Earth Objects no están disponibles en la respuesta JSON.");

			var result = asteroidData.NearEarthObjects?
				.SelectMany(neo => neo.Value)
				.Where(a => a.IsPotentiallyHazardousAsteroid)
				.Select(a => CreateAsteroidModel(a))
				.OrderByDescending(a => a.Diameter)
				.Take(3)
				.ToList()
				?? Enumerable.Empty<AsteroidModel>();

			// Almacenar en caché
			_cache.Set(jsonResponse, result, _cacheDuration);

			return result;
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
