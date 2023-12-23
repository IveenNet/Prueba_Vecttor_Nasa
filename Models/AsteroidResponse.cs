﻿namespace Prueba_Vecttor_Nasa.Models
{
	using System;
	using System.Collections.Generic;

	using System.Globalization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public partial class AsteroidResponse
	{
		[JsonProperty("links")]
		public WelcomeLinks Links { get; set; }

		[JsonProperty("element_count")]
		public long ElementCount { get; set; }

		[JsonProperty("near_earth_objects")]
		public Dictionary<string, NearEarthObject[]> NearEarthObjects { get; set; }
	}

	public partial class WelcomeLinks
	{
		[JsonProperty("next")]
		public Uri Next { get; set; }

		[JsonProperty("previous")]
		public Uri Previous { get; set; }

		[JsonProperty("self")]
		public Uri Self { get; set; }
	}

	public partial class NearEarthObject
	{
		[JsonProperty("links")]
		public NearEarthObjectLinks Links { get; set; }

		[JsonProperty("id")]
		[JsonConverter(typeof(ParseStringConverter))]
		public long Id { get; set; }

		[JsonProperty("neo_reference_id")]
		[JsonConverter(typeof(ParseStringConverter))]
		public long NeoReferenceId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("nasa_jpl_url")]
		public Uri NasaJplUrl { get; set; }

		[JsonProperty("absolute_magnitude_h")]
		public double AbsoluteMagnitudeH { get; set; }

		[JsonProperty("estimated_diameter")]
		public EstimatedDiameter EstimatedDiameter { get; set; }

		[JsonProperty("is_potentially_hazardous_asteroid")]
		public bool IsPotentiallyHazardousAsteroid { get; set; }

		[JsonProperty("close_approach_data")]
		public CloseApproachDatum[] CloseApproachData { get; set; }

		[JsonProperty("is_sentry_object")]
		public bool IsSentryObject { get; set; }
	}

	public partial class CloseApproachDatum
	{
		[JsonProperty("close_approach_date")]
		public DateTimeOffset CloseApproachDate { get; set; }

		[JsonProperty("close_approach_date_full")]
		public string CloseApproachDateFull { get; set; }

		[JsonProperty("epoch_date_close_approach")]
		public long EpochDateCloseApproach { get; set; }

		[JsonProperty("relative_velocity")]
		public RelativeVelocity RelativeVelocity { get; set; }

		[JsonProperty("miss_distance")]
		public MissDistance MissDistance { get; set; }

		[JsonProperty("orbiting_body")]
		public OrbitingBody OrbitingBody { get; set; }
	}

	public partial class MissDistance
	{
		[JsonProperty("astronomical")]
		public string Astronomical { get; set; }

		[JsonProperty("lunar")]
		public string Lunar { get; set; }

		[JsonProperty("kilometers")]
		public string Kilometers { get; set; }

		[JsonProperty("miles")]
		public string Miles { get; set; }
	}

	public partial class RelativeVelocity
	{
		[JsonProperty("kilometers_per_second")]
		public string KilometersPerSecond { get; set; }

		[JsonProperty("kilometers_per_hour")]
		public string KilometersPerHour { get; set; }

		[JsonProperty("miles_per_hour")]
		public string MilesPerHour { get; set; }
	}

	public partial class EstimatedDiameter
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

	public partial class Feet
	{
		[JsonProperty("estimated_diameter_min")]
		public double EstimatedDiameterMin { get; set; }

		[JsonProperty("estimated_diameter_max")]
		public double EstimatedDiameterMax { get; set; }
	}

	public partial class NearEarthObjectLinks
	{
		[JsonProperty("self")]
		public Uri Self { get; set; }
	}

	public enum OrbitingBody { Earth };

	public partial class Welcome
	{
		public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Prueba_Vecttor_Nasa.Models.Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Prueba_Vecttor_Nasa.Models.Converter.Settings);
	}

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

	internal class ParseStringConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			long l;
			if (Int64.TryParse(value, out l))
			{
				return l;
			}
			throw new Exception("Cannot unmarshal type long");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (long)untypedValue;
			serializer.Serialize(writer, value.ToString());
			return;
		}

		public static readonly ParseStringConverter Singleton = new ParseStringConverter();
	}
}
