using Prueba_Vecttor_Nasa.Interfaces;
using Prueba_Vecttor_Nasa.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
var serviceConfigurator = new ServiceConfigurator();
serviceConfigurator.ConfigureServices(builder.Services);

// Crear la aplicación
var app = builder.Build();

// Configurar el pipeline HTTP
var pipelineConfigurator = new PipelineConfigurator();
pipelineConfigurator.ConfigureHttpPipeline(app);

app.Run();

// Clase para configurar servicios
public class ServiceConfigurator
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddHttpClient();
		services.AddScoped<INasaService, NasaService>();
		services.AddSwaggerGen();

		// Agregar DbContext con la cadena de conexión
		//services.AddDbContext<AsteroidsContext>(options =>
		//options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
	}
}

// Clase para configurar el pipeline HTTP
public class PipelineConfigurator
{
	public void ConfigureHttpPipeline(WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwaggerConfiguration();
			app.UseRedirectToSwagger();
		}

		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();
		app.MapControllers();
	}
}

public static class SwaggerExtension
{
	public static void UseSwaggerConfiguration(this IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ASTEROIDS V1");
		});
	}
}

public static class RedirectMiddleware
{
	public static void UseRedirectToSwagger(this IApplicationBuilder app)
	{
		app.Use(async (context, next) =>
		{
			if (context.Request.Path == "/")
			{
				context.Response.Redirect("/swagger/index.html");
				return;
			}
			await next();
		});
	}
}
