namespace GoldenRaspberryAwardsApi.Dtos;

public record MovieRangeResponseDto(string Producer, int Interval, int PreviousWin, int FollowingWin);
