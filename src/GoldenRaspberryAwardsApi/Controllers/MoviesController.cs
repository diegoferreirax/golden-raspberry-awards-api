using GoldenRaspberryAwardsApi.Dtos;
using GoldenRaspberryAwardsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwardsApi.Controllers;

[ApiController]
[Route("api/v1/movies/awards-range")]
public class MoviesController(MovieService movieService) : ControllerBase
{
    private readonly MovieService _movieService = movieService;

    [HttpGet]
    public ActionResult<MovieAwardsRangeResponseDto> GetMoviesAwardsRange()
    {
        try
        {
            var moviesRange = _movieService.GetMoviesAwardsRange();
            return Ok(moviesRange);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Erro interno do servidor" });
        }
    }
}