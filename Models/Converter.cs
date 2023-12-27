using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;

namespace Prueba_Vecttor_Nasa.Models
{
	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				OrbitingBodyConverter.Singleton,
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}

}
