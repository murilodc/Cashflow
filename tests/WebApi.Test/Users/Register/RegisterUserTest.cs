using System.Net;
using System.Net.Http.Json;
using CashFlow.Api;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
{
    private const string Method = "api/User";
    
    private readonly HttpClient _httpClient;
    
    public RegisterUserTest(WebApplicationFactory<Program> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await _httpClient.PostAsJsonAsync(Method, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}