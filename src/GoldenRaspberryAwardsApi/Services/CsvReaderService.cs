using GoldenRaspberryAwardsApi.Models;

namespace GoldenRaspberryAwardsApi.Services;

public class CsvReaderService
{
    private const string CsvFileName = "movielist.csv";
    private const string FilesFolder = "Files";

    public static List<Movie> ReadAndParseCsv()
    {
        var movies = new List<Movie>();
        var csvPath = Path.Combine(Directory.GetCurrentDirectory(), FilesFolder, CsvFileName);
        
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"Arquivo não encontrado: {csvPath}");
            return movies;
        }

        var lines = File.ReadAllLines(csvPath);
        
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(';');
            if (parts.Length >= 5)
            {
                var movie = new Movie
                {
                    Year = int.TryParse(parts[0], out var year) ? year : 0,
                    Title = parts[1] ?? string.Empty,
                    Studios = parts[2] ?? string.Empty,
                    Producers = parts[3] ?? string.Empty,
                    Winner = parts[4]?.Trim().ToLower() == "yes"
                };
                movies.Add(movie);
            }
        }

        return movies;
    }

    public static void DisplayMovies(List<Movie> movies)
    {
        Console.WriteLine("=== Conteúdo do arquivo movielist.csv ===\n");
        Console.WriteLine($"Total de filmes carregados: {movies.Count}\n");
        
        foreach (var movie in movies)
        {
            Console.WriteLine($"Year: {movie.Year} | Title: {movie.Title} | Studios: {movie.Studios} | Producers: {movie.Producers} | Winner: {movie.Winner}");
        }
        
        Console.WriteLine("\n=== Fim do arquivo ===\n");
    }
}

