using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models
{
	public class EstimatedDiameter
	{
		[JsonProperty("kilometers")]
		public Feet Kilometers { get; set; }

		[JsonProperty("meters")]
		public Feet Meters { get; set; }

		[JsonProperty("miles")]
		public Feet Miles { get; set; }

		[JsonProperty("feet")]
		public Feet Feet { get; set; }
	}
}
