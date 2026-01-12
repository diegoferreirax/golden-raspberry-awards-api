using GoldenRaspberryAwardsApi.Models;
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
    public ActionResult<IEnumerable<Movie>> GetMovies()
    {
        var movies = _movieService.GetMovies();
        return Ok(movies);
    }
}

