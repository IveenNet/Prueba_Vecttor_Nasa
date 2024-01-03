using Prueba_Vecttor_Nasa.Interfaces;
using Prueba_Vecttor_Nasa.Models.DTOs;
using Prueba_Vecttor_Nasa.Services.Infrastructure.Parsers;
using Prueba_Vecttor_Nasa.Services.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace Prueba_Vecttor_Nasa.Services
{
	public class NasaService : INasaService
	{
		private readonly HttpClient _httpClient;
		private readonly NasaApiUrlBuilder _urlBuilder;
		private readonly NasaApiResponseParser _responseParser;
		private readonly IMemoryCache _cache;
		private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

		public NasaService(HttpClient httpClient, IConfiguration configuration, IMemoryCache cache)
		{
			_httpClient = httpClient;
			var nasaApiKey = configuration["API_KEY"];
			_urlBuilder = new NasaApiUrlBuilder(nasaApiKey);
			_cache = cache;
			_responseParser = new NasaApiResponseParser(_cache);
		}

		public async Task<IEnumerable<AsteroidModel>> GetAsteroidsAsync(int days)
		{
			DateTime endDate = DateTime.UtcNow;
			DateTime startDate = endDate.AddDays(-days);
			string apiUrl = _urlBuilder.BuildAsteroidApiUrl(startDate, endDate);

			// Crear una clave de caché única basada en la URL
			string cacheKey = $"asteroids-{startDate:yyyyMMdd}-{endDate:yyyyMMdd}";

			// Intentar obtener el valor del caché
			if (_cache.TryGetValue(cacheKey, out IEnumerable<AsteroidModel> cachedResult))
			{
				return cachedResult;
			}

			HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var result = _responseParser.ParseAsteroidResponse(jsonResponse);

				// Almacenar en caché
				_cache.Set(cacheKey, result, _cacheDuration);

				return result;
			}
			else
			{
				throw new HttpRequestException($"Error al obtener datos de la API de la NASA: {(int)response.StatusCode} {response.ReasonPhrase}");
			}
		}
	}
}
