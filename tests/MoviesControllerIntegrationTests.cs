using System.Net;
using System.Net.Http.Json;
using GoldenRaspberryAwardsApi.Application.Dtos;
using GoldenRaspberryAwardsApi.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Integration;

public class MoviesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public MoviesControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetMoviesAwardsRange_ShouldReturnSuccessWithValidData_WhenMoviesAreLoaded()
    {
        // Arrange
        var customFactory = _factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
        });

        var movieService = customFactory.Services.GetRequiredService<MovieService>();
        var movies = CsvReaderService.ReadAndParseCsv();
        
        Assert.NotEmpty(movies);
        Assert.True(movies.Count > 0, "O CSV deve conter pelo menos um filme");
        
        movieService.SetMovies(movies);
        
        var loadedMovies = movieService.GetMoviesAwardsRange();
        Assert.NotNull(loadedMovies);
        
        var client = customFactory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/movies/awards-range");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        var result = await response.Content.ReadFromJsonAsync<MovieAwardsRangeResponseDto>();
        Assert.NotNull(result);
        Assert.NotNull(result.Min);
        Assert.NotNull(result.Max);

        var minList = result.Min.ToList();
        var maxList = result.Max.ToList();
        Assert.NotEmpty(minList);
        Assert.NotEmpty(maxList);

        foreach (var minItem in minList)
        {
            Assert.NotNull(minItem.Producer);
            Assert.NotEmpty(minItem.Producer);
            Assert.True(minItem.Interval >= 0, "Interval deve ser maior ou igual a zero");
            Assert.True(minItem.PreviousWin > 0, "PreviousWin deve ser maior que zero");
            Assert.True(minItem.FollowingWin > 0, "FollowingWin deve ser maior que zero");
        }

        foreach (var maxItem in maxList)
        {
            Assert.NotNull(maxItem.Producer);
            Assert.NotEmpty(maxItem.Producer);
            Assert.True(maxItem.Interval >= 0, "Interval deve ser maior ou igual a zero");
            Assert.True(maxItem.PreviousWin > 0, "PreviousWin deve ser maior que zero");
            Assert.True(maxItem.FollowingWin > 0, "FollowingWin deve ser maior que zero");
        }
    }
}

