namespace GoldenRaspberryAwardsApi.Dtos;

/// <summary>
/// DTO de resposta contendo os intervalos de prêmios (menor e maior) entre produtores
/// </summary>
/// <param name="Min">Lista de produtores com o menor intervalo entre prêmios consecutivos</param>
/// <param name="Max">Lista de produtores com o maior intervalo entre prêmios consecutivos</param>
public record MovieAwardsRangeResponseDto(
    /// <summary>
    /// Lista de produtores com o menor intervalo entre prêmios consecutivos
    /// </summary>
    IEnumerable<MovieRangeResponseDto> Min,
    /// <summary>
    /// Lista de produtores com o maior intervalo entre prêmios consecutivos
    /// </summary>
    IEnumerable<MovieRangeResponseDto> Max);
