using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using WebAPI.Authentication;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Types;
using WebAPI.Tests.IntegrationTests.Setup;
using WebAPI.Tests.UnitTests;

namespace WebAPI.Tests.IntegrationTests.Controllers;

[TestFixture]
public class OrderControllerTests
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
    // Checking for null before calling Dispose
    _client?.Dispose();
    _factory?.Dispose();
  }

  [Test]
  public async Task CreateOrder_Returns_Ok_With_Valid_Order()
  {
    // Arrange
    var testModel = TestModels.TestOrderDto;
    var serverResponse = new SuccessResponse(Messages.OrderCreatedSuccess, null);
    var token = await TestToken.GenerateJwtToken(_serviceProvider);
    var content = new StringContent(JsonConvert.SerializeObject(testModel), Encoding.UTF8, "application/json");

    // Headers
    var request = new HttpRequestMessage(HttpMethod.Post, "/api/order/CreateOrder");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    request.Content = content;

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.SendAsync(request);
    var responseString = await httpResponse.Content.ReadAsStringAsync();

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
    Assert.IsTrue(responseString.Contains(Messages.OrderCreatedSuccess));
  }

  [Test]
  public async Task GetOrderList_Returns_Ok_With_User_Order_List()
  {
    // Arrange
    var userId = TestModels.TestOrderDto.UserId;
    var expectedOrderList = new List<Order>();
    var serverResponse = new SuccessResponse(Messages.GetOrderListSuccess, expectedOrderList);
    var token = await TestToken.GenerateJwtToken(_serviceProvider);

    // Headers
    var request = new HttpRequestMessage(HttpMethod.Get, $"/api/order/GetOrderList/{userId}");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetOrderListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.SendAsync(request);
    var responseString = await httpResponse.Content.ReadAsStringAsync();
    var response = JsonConvert.DeserializeObject<SuccessResponse>(responseString);
    var orders = ((JArray) response.Set)!.ToObject<List<Order>>();

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
    Assert.IsTrue(responseString.Contains(Messages.GetOrderListSuccess));

    // Additional checks
    Assert.IsNotNull(response);
    Assert.IsNotNull(response.Set);
    Assert.AreEqual(Messages.GetOrderListSuccess, response.Message);

    // Checking the presence of an identifier in each order
    Assert.IsTrue(orders.All(_ => true));
  }

  [Test]
  public async Task DeleteOrder_With_Valid_OrderId_Returns_Ok()
  {
    // Arrange
    var orderId = TestModels.TestOrderDto.Id;
    var serverResponse = new SuccessResponse(Messages.OrderDeleteSuccess, null);
    var token = await TestToken.GenerateJwtToken(_serviceProvider, "testUser");

    // Headers
    var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/order/DeleteOrder/{orderId}");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.SendAsync(request);
    var responseBody = await httpResponse.Content.ReadAsStringAsync();
    var response = JsonConvert.DeserializeObject<SuccessResponse>(responseBody);

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
    Assert.IsNotNull(response);
    Assert.IsNull(response.Set);
    Assert.AreEqual(Messages.OrderDeleteSuccess, response.Message);
  }

  [Test]
  public async Task DeleteOrder_With_Valid_OrderId_Returns_InternalServerError()
  {
    // Arrange
    var orderId = TestModels.TestOrderDto.Id;
    var serverResponse = new InternalServerError(Messages.ServerError);
    var token = await TestToken.GenerateJwtToken(_serviceProvider, "testUser");

    // Headers
    var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/order/DeleteOrder/{orderId}");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.SendAsync(request);
    var responseBody = await httpResponse.Content.ReadAsStringAsync();
    var response = JsonConvert.DeserializeObject<InternalServerError>(responseBody);

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
    Assert.IsNotNull(response);
    Assert.AreEqual(serverResponse.Message, response.Message);
    Assert.IsNull(response.Set);
  }
}
