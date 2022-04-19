using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using WebAPI.Authentication;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Types;
using WebAPI.Tests.UnitTests;

namespace WebAPI.Tests.IntegrationTests.Controllers;

[TestFixture]
public class OrderControllerTests
{
  private WebApplicationFactory<Startup> _factory;
  private HttpClient _client;
  private Mock<IMediator> _mediatorMock;
  private IConfigurationRoot _configuration;

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

    _client = _factory.CreateClient();
  }

  [TearDown]
  public void TearDown()
  {
    // Checking for null before calling Dispose
    _client?.Dispose();
    _factory?.Dispose();
  }

  [AllowAnonymous]
  [Test]
  public async Task CreateOrder_Returns_Ok_With_Valid_Order()
  {
    // Arrange
    var testModel = OrderTestModel.TestOrderDto;
    var serverResponse = new SuccessResponse(Messages.OrderCreatedSuccess, null);
    var content = new StringContent(JsonConvert.SerializeObject(testModel), Encoding.UTF8, "application/json");

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _client.PostAsync("/api/order/CreateOrder", content);
    var responseString = await response.Content.ReadAsStringAsync();

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.IsTrue(responseString.Contains(Messages.OrderCreatedSuccess));
  }

  [AllowAnonymous]
  [Test]
  public async Task GetOrderList_Returns_Ok_With_User_Order_List()
  {
    // Arrange
    var userId = "9833869f-2c8e-4986-90c8-ff256d5bc7e0";
    var expectedOrderList = new List<Order>();
    var serverResponse = new SuccessResponse(Messages.GetOrderListSuccess, expectedOrderList);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetOrderListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.GetAsync($"/api/order/GetOrderList/{userId}");
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

  [AllowAnonymous]
  [Test]
  public async Task DeleteOrder_With_Valid_OrderId_Returns_Ok()
  {
    // Arrange
    var orderId = "5B5A1E98-BC38-4387-AFCF-AB8B0125A3AC";
    var serverResponse = new SuccessResponse(Messages.OrderDeleteSuccess, null);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.DeleteAsync($"/api/order/DeleteOrder/{orderId}");
    httpResponse.EnsureSuccessStatusCode(); // The method throws an exception if the status code is not successful
    var responseBody = await httpResponse.Content.ReadAsStringAsync();
    var response = JsonConvert.DeserializeObject<SuccessResponse>(responseBody);

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
    Assert.IsNotNull(response);
    Assert.IsNull(response.Set);
    Assert.AreEqual(Messages.OrderDeleteSuccess, response.Message);
  }

  [AllowAnonymous]
  [Test]
  public async Task DeleteOrder_With_Valid_OrderId_Returns_InternalServerError()
  {
    // Arrange
    var orderId = "5B5A1E98-BC38-4387-AFCF-AB8B0125A3AC";
    var serverResponse = new InternalServerError("Internal Server Error");

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var httpResponse = await _client.DeleteAsync($"/api/order/DeleteOrder/{orderId}");
    var responseBody = await httpResponse.Content.ReadAsStringAsync();
    var response = JsonConvert.DeserializeObject<InternalServerError>(responseBody);

    // Assert
    Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
    Assert.IsNotNull(response);
    Assert.IsNull(response.Set);
    Assert.AreEqual(serverResponse.Message, response.Message);
  }
}
