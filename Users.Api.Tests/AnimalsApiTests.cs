using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
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

    [Fact]
    public async Task GetAnimalById_ReturnsAnimal()
    {
        // Arrange: Create a new animal first
        var newAnimal = new AnimalCreateRequest("Snow", "Cat", 4.2, "White");
        var postResponse = await _client.PostAsJsonAsync("/api/animals", newAnimal);
        postResponse.EnsureSuccessStatusCode();
        var createdAnimal = await postResponse.Content.ReadFromJsonAsync<AnimalResponse>();

        // Act: Retrieve the animal by its id
        var getResponse = await _client.GetAsync($"/api/animals/{createdAnimal.Id}");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var animalResponse = await getResponse.Content.ReadFromJsonAsync<AnimalResponse>();
        Assert.NotNull(animalResponse);
        Assert.Equal(createdAnimal.Id, animalResponse.Id);
    }

    [Fact]
    public async Task UpdateAnimal_UpdatesAnimal()
    {
        // Arrange: Create an animal
        var newAnimal = new AnimalCreateRequest("Max", "Dog", 20.0, "Black");
        var postResponse = await _client.PostAsJsonAsync("/api/animals", newAnimal);
        postResponse.EnsureSuccessStatusCode();
        var createdAnimal = await postResponse.Content.ReadFromJsonAsync<AnimalResponse>();

        // Act: Update animal details
        var updateRequest = new AnimalUpdateRequest("Maximus", "Dog", 22.0, "Black");
        var putResponse = await _client.PutAsJsonAsync($"/api/animals/{createdAnimal.Id}", updateRequest);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Verify update
        var getResponse = await _client.GetAsync($"/api/animals/{createdAnimal.Id}");
        getResponse.EnsureSuccessStatusCode();
        var updatedAnimal = await getResponse.Content.ReadFromJsonAsync<AnimalResponse>();
        Assert.Equal("Maximus", updatedAnimal.Name);
        Assert.Equal(22.0, updatedAnimal.Weight);
    }

    [Fact]
    public async Task DeleteAnimal_DeletesAnimal()
    {
        // Arrange: Create an animal
        var newAnimal = new AnimalCreateRequest("Bella", "Dog", 18.5, "Golden");
        var postResponse = await _client.PostAsJsonAsync("/api/animals", newAnimal);
        postResponse.EnsureSuccessStatusCode();
        var createdAnimal = await postResponse.Content.ReadFromJsonAsync<AnimalResponse>();

        // Act: Delete the animal
        var deleteResponse = await _client.DeleteAsync($"/api/animals/{createdAnimal.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Try to get the deleted animal.
        var getResponse = await _client.GetAsync($"/api/animals/{createdAnimal.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}