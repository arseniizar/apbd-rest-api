using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responses;
using Xunit;

namespace Users.Api.Tests;

public class AnimalsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AnimalsApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAnimals_ReturnsOkAndList()
    {
        // Act
        var response = await _client.GetAsync("/api/animals");

        // Assert
        response.EnsureSuccessStatusCode();
        var animals = await response.Content.ReadFromJsonAsync<List<AnimalResponse>>();
        Assert.NotNull(animals);
    }

    [Fact]
    public async Task PostAnimal_CreatesAnimal()
    {
        // Arrange
        var newAnimal = new AnimalCreateRequest("Fluffy", "Cat", 3.5, "White");

        // Act
        var response = await _client.PostAsJsonAsync("/api/animals", newAnimal);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var animalResponse = await response.Content.ReadFromJsonAsync<AnimalResponse>();
        Assert.NotNull(animalResponse);
        Assert.Equal("Fluffy", animalResponse.Name);
    }
}