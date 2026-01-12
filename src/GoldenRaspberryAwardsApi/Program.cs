using GoldenRaspberryAwardsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
