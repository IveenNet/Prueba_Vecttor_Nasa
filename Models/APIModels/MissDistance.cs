using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models.APIModels
{
    public class MissDistance
    {
        [JsonProperty("astronomical")]
        public string? Astronomical { get; set; }

        [JsonProperty("lunar")]
        public string? Lunar { get; set; }

        [JsonProperty("kilometers")]
        public string? Kilometers { get; set; }

        [JsonProperty("miles")]
        public string? Miles { get; set; }
    }
}
