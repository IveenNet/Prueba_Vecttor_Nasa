namespace Prueba_Vecttor_Nasa.Models.DTOs
{
    public class AsteroidModel
    {
        public string? Name { get; set; }
        public double Diameter { get; set; }
        public string? Velocity { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string? Planet { get; set; }
    }
}

