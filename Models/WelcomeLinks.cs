using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models
{
	public class WelcomeLinks
	{
		[JsonProperty("next")]
		public Uri Next { get; set; }

		[JsonProperty("previous")]
		public Uri Previous { get; set; }

		[JsonProperty("self")]
		public Uri Self { get; set; }
	}
}
