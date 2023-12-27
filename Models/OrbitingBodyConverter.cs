using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models
{
	internal class OrbitingBodyConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(OrbitingBody) || t == typeof(OrbitingBody?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "Earth")
			{
				return OrbitingBody.Earth;
			}
			throw new Exception("Cannot unmarshal type OrbitingBody");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (OrbitingBody)untypedValue;
			if (value == OrbitingBody.Earth)
			{
				serializer.Serialize(writer, "Earth");
				return;
			}
			throw new Exception("Cannot marshal type OrbitingBody");
		}

		public static readonly OrbitingBodyConverter Singleton = new OrbitingBodyConverter();
	}
}
