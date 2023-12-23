using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models
{
	public class CloseApproachData
	{
		[JsonProperty("close_approach_date")]
		public string CloseApproachDate { get; set; }

		[JsonProperty("close_approach_date_full")]
		public string CloseApproachDateFull { get; set; }

		[JsonProperty("epoch_date_close_approach")]
		public long EpochDateCloseApproach { get; set; }

		// Otros campos como velocidad relativa, distancia, etc.
	}

}
