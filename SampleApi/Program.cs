using Exo_Linq_Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using SampleApi.Entities;
using SampleApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.

builder.Services.AddControllers();

//INJECTION DEPENDANCES



IContext context = new DataContext();

//1 seule boite à outils pour tous => Attention aux partage des outils
//builder.Services.AddSingleton<IContext>(s=> context );
//1 boite à outils tant que je reste sur le site et si un autre 
//arrive il a sa boite à outils pour travailler
builder.Services.AddScoped<IContext>(s => context);
// 1 boite à outil par action ==> A chaque coup de marteau je change de marteau
//builder.Services.AddTransient<IContext>(s => context);

string cnstr= builder.Configuration.GetConnectionString("Dev");
builder.Services.AddScoped<IRepository<StudentPOCO>, StudentRepository>(s=> new StudentRepository(cnstr));
builder.Services.AddScoped<IRepository<SectionPOCO>, SectionRepository>(s => new SectionRepository(cnstr));




//Gestion du versionning
builder.Services.AddApiVersioning(opt =>
{
	opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
	opt.AssumeDefaultVersionWhenUnspecified = true;
	opt.ReportApiVersions = true;
	opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
													new HeaderApiVersionReader("x-api-version"),
													new MediaTypeApiVersionReader("x-api-version"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddVersionedApiExplorer(setup =>
{
	setup.GroupNameFormat = "'v'VVV";
	setup.SubstituteApiVersionInUrl = true;
});
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Sample API",
		Description = "Exemple simple d'API", 
		Contact = new OpenApiContact
		{
			Name = "Cognitic Sprl",
			Url = new Uri("https://www.cognitic.be")
		} 
	});
	options.SwaggerDoc("v2", new OpenApiInfo
	{
		Version = "v2",
		Title = "Sample API V2",
		Description = "Exemple simple d'API",
		Contact = new OpenApiContact
		{
			Name = "Cognitic Sprl",
			Url = new Uri("https://www.cognitic.be")
		}
	});
	 

	
});

var app = builder.Build();
 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
	app.UseSwaggerUI(options =>
	{

		options.DocumentTitle = "Sample Api";
		options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
		options.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
