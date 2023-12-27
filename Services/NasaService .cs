using Newtonsoft.Json;
using Prueba_Vecttor_Nasa.Controllers;
using Prueba_Vecttor_Nasa.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prueba_Vecttor_Nasa.Services
{
	public class NasaService : INasaService
	{
		private readonly HttpClient _httpClient;
		private readonly string _nasaApiKey;

		// Constructor que inicializa HttpClient y obtiene la clave API de la configuración
		public NasaService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_nasaApiKey = configuration["API_KEY"];
		}

		// Método para obtener asteroides de la API de la NASA en base a la cantidad de días
		public async Task<IEnumerable<NearEarthObject>> GetAsteroidsAsync(int days)
		{
			// Calcula la fecha de inicio y fin basada en los días proporcionados
			DateTime endDate = DateTime.UtcNow;
			DateTime startDate = endDate.AddDays(-days);

			// Construye la URL de la API con las fechas y la clave API
			string apiUrl = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={_nasaApiKey}";

			// Realiza la solicitud HTTP
			HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				// Lee y deserializa la respuesta JSON si la solicitud fue exitosa
				string jsonResponse = await response.Content.ReadAsStringAsync();
				var asteroidData = JsonConvert.DeserializeObject<AsteroidResponse>(jsonResponse);

				// Si hay datos de asteroides, los devuelve
				if (asteroidData?.NearEarthObjects != null)
				{
					return asteroidData.NearEarthObjects.SelectMany(neo => neo.Value);
				}
				else
				{
					// Si no hay datos, devuelve una lista vacía
					return Enumerable.Empty<NearEarthObject>();
				}
			}
			else
			{
				// Maneja la respuesta de error lanzando una excepción
				throw new HttpRequestException($"Error al obtener datos de la API de la NASA: {(int)response.StatusCode} {response.ReasonPhrase}");
			}
		}
	}
}
