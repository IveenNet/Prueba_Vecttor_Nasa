namespace Prueba_Vecttor_Nasa.Models
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	public partial class AsteroidResponse
	{
		[JsonProperty("links")]
		public WelcomeLinks Links { get; set; }

		[JsonProperty("element_count")]
		public long ElementCount { get; set; }

		[JsonProperty("near_earth_objects")]
		public Dictionary<string, NearEarthObject[]> NearEarthObjects { get; set; }
	}
}
