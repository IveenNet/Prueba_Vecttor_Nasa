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
		private readonly string _nasaApiKey = "zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb"; // Deberías obtener esto de tus configuraciones

		public NasaService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<NearEarthObject>> GetAsteroidsAsync(int numberOfDays)
		{
			// Assuming you have startDate and endDate calculated based on numberOfDays
			DateTime endDate = DateTime.UtcNow;
			DateTime startDate = endDate.AddDays(-numberOfDays);

			string apiUrl = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={_nasaApiKey}";

			HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialize JSON to your model
				var asteroidData = JsonConvert.DeserializeObject<AsteroidResponse>(jsonResponse);
				if (asteroidData?.NearEarthObjects != null)
				{
					// Flatten the dictionary values and return them.
					return asteroidData.NearEarthObjects.SelectMany(neo => neo.Value);
				}
				else
				{
					// Handle the case when NearEarthObjects is null
					return Enumerable.Empty<NearEarthObject>();
				}
			}
			else
			{
				// Handle error response (e.g., throw an exception or return an empty list)
				throw new HttpRequestException($"Error fetching data from NASA API: {(int)response.StatusCode} {response.ReasonPhrase}");
			}
		}

	}
}
