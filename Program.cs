using Prueba_Vecttor_Nasa.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
ConfigureServices(builder.Services);

// Crear la aplicación
var app = builder.Build();

// Configurar el pipeline HTTP
ConfigureHttpPipeline(app);

app.Run();

// Método para configurar servicios
void ConfigureServices(IServiceCollection services)
{
	services.AddControllers();
	services.AddEndpointsApiExplorer();
	services.AddHttpClient();
	services.AddScoped<INasaService, NasaService>();
	services.AddSwaggerGen();
}

// Método para configurar el pipeline HTTP
void ConfigureHttpPipeline(WebApplication app)
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();
	app.UseAuthorization();
	app.MapControllers();
}
