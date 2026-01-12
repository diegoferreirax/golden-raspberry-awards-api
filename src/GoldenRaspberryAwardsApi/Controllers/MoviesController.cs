using GoldenRaspberryAwardsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwardsApi.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;

    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public ActionResult<MovieAwardsRangeResponseDto> GetMovies()
    {
        var movies = _movieService.GetMovies();

        var producerWins = new Dictionary<string, List<int>>();
        foreach (var w in movies)
        {
            var producers = w.Producers.Split(new[] { ",", " and " }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var producer in producers)
            {
                if (!producerWins.ContainsKey(producer))
                {
                    producerWins[producer] = new List<int>();
                }
                producerWins[producer].Add(w.Year);
            }
        }

        var moviesRangeDto = new List<MovieRangeResponseDto>();
        foreach (var kvp in producerWins)
        {
            var producer = kvp.Key;
            var years = kvp.Value;
            var wins = years.Count;

            if (wins > 1)
            {
                moviesRangeDto.Add(new MovieRangeResponseDto(
                    producer: producer,
                    interval: years[1] - years[0],
                    previousWin: years[0],
                    followingWin: years[1]));
            }
        }

        var minInterval = moviesRangeDto.OrderBy(m => m.interval).First().interval;
        var maxInterval = moviesRangeDto.OrderBy(m => m.interval).Last().interval;

        var movieAwardsRangeResponseDto = new MovieAwardsRangeResponseDto(
            min: moviesRangeDto.Where(m => m.interval == minInterval).ToList(),
            max: moviesRangeDto.Where(m => m.interval == maxInterval).ToList());

        return Ok(movieAwardsRangeResponseDto);
    }
}

public record MovieAwardsRangeResponseDto(IEnumerable<MovieRangeResponseDto> min, IEnumerable<MovieRangeResponseDto> max);

public record MovieRangeResponseDto(string producer, int interval, int previousWin, int followingWin);
