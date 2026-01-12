namespace GoldenRaspberryAwardsApi.Dtos;

public record MovieAwardsRangeResponseDto(IEnumerable<MovieRangeResponseDto> Min, IEnumerable<MovieRangeResponseDto> Max);
