namespace GoldenRaspberryAwardsApi.Dtos;

/// <summary>
/// DTO representando o intervalo de prêmios de um produtor
/// </summary>
/// <param name="Producer">Nome do produtor</param>
/// <param name="Interval">Intervalo em anos entre os prêmios consecutivos</param>
/// <param name="PreviousWin">Ano do prêmio anterior</param>
/// <param name="FollowingWin">Ano do prêmio seguinte</param>
public record MovieRangeResponseDto(
    /// <summary>
    /// Nome do produtor
    /// </summary>
    string Producer,
    /// <summary>
    /// Intervalo em anos entre os prêmios consecutivos
    /// </summary>
    int Interval,
    /// <summary>
    /// Ano do prêmio anterior
    /// </summary>
    int PreviousWin,
    /// <summary>
    /// Ano do prêmio seguinte
    /// </summary>
    int FollowingWin);
