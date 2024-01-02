namespace Prueba_Vecttor_Nasa.Services.Infrastructure
{
	public class NasaApiUrlBuilder
	{
		private readonly string _baseApiUrl = "https://api.nasa.gov/neo/rest/v1/feed";
		private readonly string _nasaApiKey;

		public NasaApiUrlBuilder(string nasaApiKey)
		{
			_nasaApiKey = nasaApiKey;
		}

		public string BuildAsteroidApiUrl(DateTime startDate, DateTime endDate)
		{
			return $"{_baseApiUrl}?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={_nasaApiKey}";
		}
	}

}
