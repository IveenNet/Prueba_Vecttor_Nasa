using Newtonsoft.Json;
using System;

namespace Prueba_Vecttor_Nasa.Models
{
	public static class Serialize
	{
		public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}
}
