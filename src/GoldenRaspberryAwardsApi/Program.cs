using GoldenRaspberryAwardsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Golden Raspberry Awards API",
        Version = "v1",
        Description = "API REST que possibilita a leitura da lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Golden Raspberry Awards API"
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddSingleton<MovieService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var movieService = app.Services.GetRequiredService<MovieService>();
var movies = CsvReaderService.ReadAndParseCsv();
movieService.SetMovies(movies);

app.Run();

public partial class Program { }
