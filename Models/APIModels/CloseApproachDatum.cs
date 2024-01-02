using Newtonsoft.Json;
using Prueba_Vecttor_Nasa.Models.Enums;

namespace Prueba_Vecttor_Nasa.Models.APIModels
{
    public class CloseApproachDatum
    {
        [JsonProperty("close_approach_date")]
        public DateTimeOffset CloseApproachDate { get; set; }

        [JsonProperty("close_approach_date_full")]
        public string? CloseApproachDateFull { get; set; }

        [JsonProperty("epoch_date_close_approach")]
        public long EpochDateCloseApproach { get; set; }

        [JsonProperty("relative_velocity")]
        public RelativeVelocity RelativeVelocity { get; set; }

        [JsonProperty("miss_distance")]
        public MissDistance? MissDistance { get; set; }

        [JsonProperty("orbiting_body")]
        public OrbitingBody OrbitingBody { get; set; }
    }
}
