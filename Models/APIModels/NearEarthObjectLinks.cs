using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models.APIModels
{
    public class NearEarthObjectLinks
    {
        [JsonProperty("self")]
        public Uri? Self { get; set; }
    }
}
