using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models
{
	public class Feet
	{
		[JsonProperty("estimated_diameter_min")]
		public double EstimatedDiameterMin { get; set; }

		[JsonProperty("estimated_diameter_max")]
		public double EstimatedDiameterMax { get; set; }
	}
}
