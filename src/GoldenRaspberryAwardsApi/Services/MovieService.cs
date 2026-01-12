using GoldenRaspberryAwardsApi.Models;

namespace GoldenRaspberryAwardsApi.Services;

public class MovieService
{
    private readonly List<Movie> _movies = new();

    public List<Movie> GetMovies()
    {
        return _movies;
    }

    public void SetMovies(List<Movie> movies)
    {
        _movies.Clear();
        _movies.AddRange(movies);
    }
}

