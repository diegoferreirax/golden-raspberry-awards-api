using GoldenRaspberryAwardsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwardsApi.Controllers;

[ApiController]
[Route("api/movies/awards-range")]
public class MoviesController(MovieService movieService) : ControllerBase
{
    private readonly MovieService _movieService = movieService;

    [HttpGet]
    public ActionResult<MovieAwardsRangeResponseDto> GetMovies()
    {
        var moviesRange = _movieService.GetMoviesAwardsRange();
        return Ok(moviesRange);
    }
}

public record MovieAwardsRangeResponseDto(IEnumerable<MovieRangeResponseDto> Min, IEnumerable<MovieRangeResponseDto> Max);

public record MovieRangeResponseDto(string Producer, int Interval, int PreviousWin, int FollowingWin);
