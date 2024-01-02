namespace Prueba_Vecttor_Nasa.Models.Entities
{
	public class TopAsteroids
	{
		public int TopAsteroidsId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public List<Asteroid>? Asteroids { get; set; }
	}
}
