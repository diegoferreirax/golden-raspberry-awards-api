using GoldenRaspberryAwardsApi.Controllers;
using GoldenRaspberryAwardsApi.Models;

namespace GoldenRaspberryAwardsApi.Services;

public class MovieService
{
    private readonly List<Movie> _movies = [];

    public MovieAwardsRangeResponseDto GetMoviesAwardsRange()
    {
        var producerWins = _getProducerWins(_movies);
        var moviesRangeDto = _getMoviesRangeDto(producerWins);
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
        _movies.Clear();
        _movies.AddRange(movies);
    }

    private static Dictionary<string, List<int>> _getProducerWins(List<Movie> movies)
    {
        var producerWins = new Dictionary<string, List<int>>();
        foreach (var w in movies)
        {
            var producers = w.Producers.Split([",", " and "], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var producer in producers)
            {
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

    private static List<MovieRangeResponseDto> _getMoviesRangeDto(Dictionary<string, List<int>> producerWins)
    {
        var moviesRangeDto = new List<MovieRangeResponseDto>();
        foreach (var kvp in producerWins)
        {
            var producer = kvp.Key;
            var years = kvp.Value;
            var wins = years.Count;
            if (wins > 1)
            {
                moviesRangeDto.Add(new MovieRangeResponseDto(
                    Producer: producer,
                    Interval: years[1] - years[0],
                    PreviousWin: years[0],
                    FollowingWin: years[1]));
            }
        }
        return moviesRangeDto;
    }
}

