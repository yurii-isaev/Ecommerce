using System.Collections.Generic;
using System.IO;
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
using NUnit.Framework;
using WebAPI.Authentication;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Types;
using WebAPI.Tests.Setup;

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
    var testOrderDto = new OrderDto
    {
      Subtotal = 200.23412341m,
      Tax = 0m,
      Total = 200.23412341m,
      Discount = 0m,
      Quantity = 1,
      UserId = "9833869f-2c8e-4986-90c8-ff256d5bc7e0",
      IsPaid = true,
      OrderAddress = null,
      OrderDetails = new List<OrderDetailsDto>
      {
        new OrderDetailsDto
        {
          Article = "000405",
          Avalible = true,
          Category = "cake",
          Filling = "fruits",
          Image = "tired_pear_cake.jpg",
          ImageSlice = "tired_pear_cake_slice.jpg",
          Name = "Tired Pear Cake",
          Price = 200.23412341m,
          Quantity = 1,
          Tier = 1,
          Weight = 1400
        }
      },
      OrderCardPayment = new OrderCardPaymentDto
      {
        CardHolder = "Test",
        CardNumber = "1111222233334444",
        ExpMonth = "12",
        ExpYear = 2027,
        Cvv = "261",
        UserId = "9833869f-2c8e-4986-90c8-ff256d5bc7e0"
      }
    };

    var serverResponse = new SuccessResponse(Messages.OrderCreatedSuccess, null);
    var content = new StringContent(JsonConvert.SerializeObject(testOrderDto), Encoding.UTF8, "application/json");

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
}
