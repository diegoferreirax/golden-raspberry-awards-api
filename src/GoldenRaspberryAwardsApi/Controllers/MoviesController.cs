using GoldenRaspberryAwardsApi.Application.Dtos;
using GoldenRaspberryAwardsApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwardsApi.Controllers;

/// <summary>
/// Controller para gerenciar informações sobre filmes do Golden Raspberry Awards
/// </summary>
[ApiController]
[Route("api/v1/movies/awards-range")]
public class MoviesController(MovieService movieService) : ControllerBase
{
    private readonly MovieService _movieService = movieService;

    /// <summary>
    /// Obtém os intervalos de prêmios (menor e maior) entre produtores que ganharam múltiplos prêmios
    /// </summary>
    /// <returns>Objeto contendo os produtores com menor e maior intervalo entre prêmios consecutivos</returns>
    /// <response code="200">Retorna os intervalos de prêmios com sucesso</response>
    /// <response code="404">Nenhum filme vencedor ou intervalo encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    /// <remarks>
    /// Este endpoint analisa todos os filmes vencedores do Golden Raspberry Awards e identifica:
    /// - Os produtores com o menor intervalo entre prêmios consecutivos (Min)
    /// - Os produtores com o maior intervalo entre prêmios consecutivos (Max)
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(MovieAwardsRangeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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