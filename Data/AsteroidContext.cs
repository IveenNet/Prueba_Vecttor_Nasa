using Microsoft.EntityFrameworkCore;
using Prueba_Vecttor_Nasa.Models.Entities;

namespace Prueba_Vecttor_Nasa.Contexts
{
    public class AsteroidContext : DbContext
	{
		public DbSet<Asteroid> Asteroids { get; set; }
		public DbSet<TopAsteroids> TopAsteroidPeriods { get; set; }

		public AsteroidContext(DbContextOptions<AsteroidContext> options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Aquí podriamos configurar las relaciones y llaves primarias
		}
	}
}
