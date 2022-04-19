using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using WebAPI.Authentication;
using WebAPI.Authentication.UseCases.Models.Output;

namespace WebAPI.Tests.IntegrationTests.Controllers;

[TestFixture]
public class StoreControllerTests
{
  private WebApplicationFactory<Startup> _factory;

  [OneTimeSetUp]
  public void SetupFixture()
  {
    _factory = new WebApplicationFactory<Startup>();
  }

  [OneTimeTearDown]
  public void TearDownFixture()
  {
    _factory.Dispose();
  }

  [Test]
  public async Task GetAllProducts_Returns_Success_Response()
  {
    // Arrange
    // Creating an HTTP client to send requests to the application
    var client = _factory.CreateClient(); 

    // Act
    var response = await client.GetAsync("/api/store/GetAllProducts?pageNumber=1&pageSize=10");
    var content = await response.Content.ReadAsStringAsync();
    // Deserializing a JSON response into a ServerResponse object
    var serverResponse = JsonSerializer.Deserialize<ServerResponse>(content, new JsonSerializerOptions 
    {
      PropertyNameCaseInsensitive = true
    });
    
    // Assert
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.IsNotNull(serverResponse);
    Assert.IsTrue(serverResponse.Success);
    Assert.IsNotNull(serverResponse.Set);
  }
}
