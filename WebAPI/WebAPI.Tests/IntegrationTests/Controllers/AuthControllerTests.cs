using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using WebAPI.Authentication;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Types;
using WebAPI.Tests.IntegrationTests.Setup;
using WebAPI.Tests.UnitTests;

namespace WebAPI.Tests.IntegrationTests.Controllers;

public class AuthControllerTests
{
  private WebApplicationFactory<Startup> _factory;
  private HttpClient _client;
  private Mock<IMediator> _mediatorMock;
  private IConfigurationRoot _configuration;
  private IServiceProvider _serviceProvider;

  [SetUp]
  public void Setup()
  {
    // Creating a mock of IMediator
    _mediatorMock = new Mock<IMediator>();

    // Creating a configuration
    _configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

    _factory = new DbConnectionFactory<Startup>(_configuration).WithWebHostBuilder(builder =>
    {
      builder.ConfigureServices(services => services.AddScoped(_ => _mediatorMock.Object));
    });

    _serviceProvider = _factory.Services;
    _client = _factory.CreateClient();
  }

  [TearDown]
  public void TearDown()
  {
    _client?.Dispose();
    _factory?.Dispose();
  }

  [Test]
  public async Task SignUp_Should_Return_Ok_When_Registration_Succeeds()
  {
    // Arrange
    var registerDto = TestModels.TestRegisterDto;
    var serverResponse = new SuccessResponse(Messages.AuthSuccess, registerDto);
    var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.PostAsync("/api/auth/SignUp", content);
    var responseBody = await httpResponse.Content.ReadAsStringAsync();
    httpResponse.EnsureSuccessStatusCode();
    var response = JsonConvert.DeserializeObject<SuccessResponse>(responseBody);

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
    Assert.IsNotNull(response);
    Assert.IsNotNull(response.Set);
    Assert.AreEqual(Messages.AuthSuccess, response.Message);
  }

  [Test]
  public async Task SignUp_Should_Return_InternalServerError_When_Role_Does_Not_Exist()
  {
    // Arrange
    var registerDto = TestModels.TestRegisterDto;
    var serverResponse = new InternalServerError("Role CUSTOMER does not exist.");
    var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.PostAsync("/api/auth/SignUp", content);
    var responseBody = await httpResponse.Content.ReadAsStringAsync();
    var response = JsonConvert.DeserializeObject<InternalServerError>(responseBody);

    // Assert
    Assert.AreEqual((int) HttpStatusCode.InternalServerError, response.Code);
    Assert.IsNotNull(response);
    Assert.AreEqual("Role CUSTOMER does not exist.", response.Message);
  }

  [Authorize(Roles = "Customer")]
  [Test]
  public async Task GetAuthProfile_Returns_Profile_When_User_Is_Authenticated_By_Token()
  {
    // Arrange
    var jwtToken = await TestToken.GenerateJwtToken(_serviceProvider);
    var expectedProfile = TestModels.TestProfileDto;

    var request = new HttpRequestMessage(HttpMethod.Get, "/api/auth/GetAuthProfile");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetAuthProfileQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(new SuccessResponse(Messages.AuthSuccess, new { profile = expectedProfile }));
    
    // Act
    var response = await _client.SendAsync(request);
    var responseString = await response.Content.ReadAsStringAsync();
    response.EnsureSuccessStatusCode();

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.IsTrue(responseString.Contains("profile"));
  }

  [Authorize(Roles = "Customer")]
  [Test]
  public async Task GetAuthProfile_Returns_Unauthorized_When_User_Is_Not_Authenticated()
  {
    // Arrange
    var request = new HttpRequestMessage(HttpMethod.Get, "/api/auth/GetAuthProfile");

    // Act
    var response = await _client.SendAsync(request);

    // Assert
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }
}
