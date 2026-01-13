using GoldenRaspberryAwardsApi.Dtos;
using GoldenRaspberryAwardsApi.Models;

namespace GoldenRaspberryAwardsApi.Services;

public class MovieService
{
    private readonly List<Movie> _movies = [];

    public MovieAwardsRangeResponseDto GetMoviesAwardsRange()
    {
        if (!_movies.Any())
            throw new InvalidOperationException("Nenhum filme carregado.");

        var producerWins = _getProducerWins(_movies);
        if (!producerWins.Any())
            throw new InvalidOperationException("Nenhum filme vencedor encontrado.");

        var moviesRangeDto = _calculateAllIntervals(producerWins);
        if (!moviesRangeDto.Any())
            throw new InvalidOperationException("Nenhum intervalo de prêmios encontrado.");

        var moviesRangeDtoOrdered = moviesRangeDto.OrderBy(m => m.Interval).ToList();

        var minInterval = moviesRangeDtoOrdered.First().Interval;
        var maxInterval = moviesRangeDtoOrdered.Last().Interval;

        var movieAwardsRangeResponseDto = new MovieAwardsRangeResponseDto(
            Min: [.. moviesRangeDto.Where(m => m.Interval == minInterval)],
            Max: [.. moviesRangeDto.Where(m => m.Interval == maxInterval)]);

        return movieAwardsRangeResponseDto;
    }

    public void SetMovies(List<Movie> movies)
    {
        if (movies == null)
            throw new ArgumentNullException(nameof(movies), "A lista de filmes não pode ser nula.");

        var validMovies = movies.Where(m => IsValidMovie(m)).ToList();
        
        if (validMovies.Count < movies.Count)
        {
            var invalidCount = movies.Count - validMovies.Count;
            Console.WriteLine($"Aviso: {invalidCount} filme(s) inválido(s) foram ignorados.");
        }

        _movies.Clear();
        _movies.AddRange(validMovies);
    }

    private static bool IsValidMovie(Movie movie)
    {
        if (movie == null)
            return false;

        if (movie.Year < 1900 || movie.Year > 2100)
            return false;

        if (string.IsNullOrWhiteSpace(movie.Title))
            return false;

        if (string.IsNullOrWhiteSpace(movie.Producers))
            return false;

        return true;
    }

    private static Dictionary<string, List<int>> _getProducerWins(List<Movie> movies)
    {
        var producerWins = new Dictionary<string, List<int>>();
        foreach (var w in movies)
        {
            if (string.IsNullOrWhiteSpace(w.Producers))
                continue;

            var producers = w.Producers.Split([",", " and "], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var producer in producers)
            {
                if (string.IsNullOrWhiteSpace(producer))
                    continue;

                if (!producerWins.TryGetValue(producer, out var value))
                {
                    value = [];
                    producerWins[producer] = value;
                }
                value.Add(w.Year);
            }
        }
        return producerWins;
    }

    private static List<MovieRangeResponseDto> _calculateAllIntervals(
    Dictionary<string, List<int>> producerWins)
    {
        var intervals = new List<MovieRangeResponseDto>();

        foreach (var kvp in producerWins)
        {
            var producer = kvp.Key;
            var years = kvp.Value.OrderBy(y => y).ToList();

            for (int i = 0; i < years.Count - 1; i++)
            {
                intervals.Add(new MovieRangeResponseDto(
                    Producer: producer,
                    Interval: years[i + 1] - years[i],
                    PreviousWin: years[i],
                    FollowingWin: years[i + 1]));
            }
        }

        return intervals;
    }
}

