namespace Prueba_Vecttor_Nasa.Models.Entities
{
	public class Asteroid
	{
		public int AsteroidId { get; set; }
		public string? Name { get; set; }
		public double Diameter { get; set; }
		public double Velocity { get; set; }
		public DateTime CloseApproachDate { get; set; }
		public string? OrbitingPlanet { get; set; }

		public int TopAsteroidsId { get; set; }
		public TopAsteroids? TopAsteroids { get; set; }
	}
}
