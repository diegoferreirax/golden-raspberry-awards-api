using GoldenRaspberryAwardsApi.Domain;

namespace GoldenRaspberryAwardsApi.Application.Services;

public class CsvReaderService
{
    private const string CsvFileName = "movielist.csv";
    private const string FilesFolder = "Files";

    public static List<Movie> ReadAndParseCsv()
    {
        try
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
                    if (!int.TryParse(parts[0], out var year) || !IsValidYear(year))
                    {
                        Console.WriteLine($"Linha {i + 1}: Ano inválido '{parts[0]}'. Filme ignorado.");
                        continue;
                    }

                    var title = parts[1]?.Trim() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine($"Linha {i + 1}: Título vazio. Filme ignorado.");
                        continue;
                    }

                    var producers = parts[3]?.Trim() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(producers))
                    {
                        Console.WriteLine($"Linha {i + 1}: Produtores vazios. Filme ignorado.");
                        continue;
                    }

                    var movie = new Movie
                    {
                        Year = year,
                        Title = title,
                        Studios = parts[2]?.Trim() ?? string.Empty,
                        Producers = producers,
                        Winner = parts[4]?.Trim().ToLower() == "yes"
                    };
                    movies.Add(movie);
                }
                else
                {
                    Console.WriteLine($"Linha {i + 1}: Formato inválido (esperado 5 colunas, encontrado {parts.Length}). Linha ignorada.");
                }
            }

            return movies;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar arquivo CSV: {ex.Message}");
            return new List<Movie>();
        }
    }

    private static bool IsValidYear(int year)
    {
        return year >= 1900 && year <= 2100;
    }
}

