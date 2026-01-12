namespace GoldenRaspberryAwardsApi.Models;

public class Movie
{
    public int Year { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Studios { get; set; } = string.Empty;
    public string Producers { get; set; } = string.Empty;
    public bool Winner { get; set; }
}

