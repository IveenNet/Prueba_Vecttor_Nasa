using Newtonsoft.Json;
using Prueba_Vecttor_Nasa.Models.Utilities;

namespace Prueba_Vecttor_Nasa.Models.APIModels
{
    public class NearEarthObject
    {
        [JsonProperty("links")]
        public NearEarthObjectLinks? Links { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("neo_reference_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NeoReferenceId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("nasa_jpl_url")]
        public Uri? NasaJplUrl { get; set; }

        [JsonProperty("absolute_magnitude_h")]
        public double AbsoluteMagnitudeH { get; set; }

        [JsonProperty("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; }

        [JsonProperty("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; }

        [JsonProperty("close_approach_data")]
        public CloseApproachDatum[]? CloseApproachData { get; set; }

        [JsonProperty("is_sentry_object")]
        public bool IsSentryObject { get; set; }
    }
}
