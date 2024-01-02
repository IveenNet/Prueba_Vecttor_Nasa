using Prueba_Vecttor_Nasa.Interfaces;
using Prueba_Vecttor_Nasa.Models.DTOs;
using Prueba_Vecttor_Nasa.Services.Infrastructure.Parsers;
using Prueba_Vecttor_Nasa.Services.Infrastructure;

namespace Prueba_Vecttor_Nasa.Services
{
	public class NasaService : INasaService
	{
		private readonly HttpClient _httpClient;
		private readonly NasaApiUrlBuilder _urlBuilder;
		private readonly NasaApiResponseParser _responseParser;

		public NasaService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			var nasaApiKey = configuration["API_KEY"];
			_urlBuilder = new NasaApiUrlBuilder(nasaApiKey);
			_responseParser = new NasaApiResponseParser();
		}

		public async Task<IEnumerable<AsteroidModel>> GetAsteroidsAsync(int days)
		{
			DateTime endDate = DateTime.UtcNow;
			DateTime startDate = endDate.AddDays(-days);

			string apiUrl = _urlBuilder.BuildAsteroidApiUrl(startDate, endDate);

			HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return _responseParser.ParseAsteroidResponse(jsonResponse);
			}
			else
			{
				throw new HttpRequestException($"Error al obtener datos de la API de la NASA: {(int)response.StatusCode} {response.ReasonPhrase}");
			}
		}
	}
}
