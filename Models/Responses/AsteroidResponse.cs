namespace Prueba_Vecttor_Nasa.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Prueba_Vecttor_Nasa.Models.APIModels;

    public partial class AsteroidResponse
    {
        [JsonProperty("links")]
        public Links? Links { get; set; }

        [JsonProperty("element_count")]
        public long ElementCount { get; set; }

        [JsonProperty("near_earth_objects")]
        public Dictionary<string, NearEarthObject[]>? NearEarthObjects { get; set; }
    }
}
