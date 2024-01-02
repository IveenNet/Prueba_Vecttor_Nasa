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

			var asteroidData = JsonConvert.DeserializeObject<AsteroidResponse>(jsonResponse);
			if (asteroidData?.NearEarthObjects == null)
			{
				throw new InvalidOperationException("Los datos de Near Earth Objects no están disponibles en la respuesta JSON.");
			}

			return asteroidData.NearEarthObjects
				.SelectMany(neo => neo.Value)
				.Where(a => a.IsPotentiallyHazardousAsteroid)
				.Select(a => new AsteroidModel
				{
					Name = a.Name,
					Diameter = CalculateAverageDiameter(a.EstimatedDiameter.Kilometers),
					Velocity = a.CloseApproachData.FirstOrDefault()?.RelativeVelocity.KilometersPerHour,
					Date = a.CloseApproachData.FirstOrDefault()?.CloseApproachDate,
					Planet = a.CloseApproachData.FirstOrDefault()?.OrbitingBody.ToString() // Asegúrate de que OrbitingBody no sea nulo
				});
		}

		private double CalculateAverageDiameter(Feet diameter)
		{
			return (diameter.EstimatedDiameterMin + diameter.EstimatedDiameterMax) / 2;
		}

	}
}
