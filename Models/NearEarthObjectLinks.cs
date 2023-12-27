using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models
{
	public class NearEarthObjectLinks
	{
		[JsonProperty("self")]
		public Uri Self { get; set; }
	}
}
