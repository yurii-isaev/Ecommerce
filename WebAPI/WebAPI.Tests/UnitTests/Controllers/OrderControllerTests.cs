using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Authentication.Controllers;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Tests.UnitTests.Controllers;

[TestFixture]
public class OrderControllerTests
{
  private Mock<IMediator> _mediatorMock;
  private OrderController _orderController;

  [SetUp]
  public void Setup()
  {
    _mediatorMock = new Mock<IMediator>();
    _orderController = new OrderController(_mediatorMock.Object);
  }

  [Test]
  public async Task GetOrderList_Returns_OkResult_With_User_Order_List()
  {
    // Arrange
    var userId = "9833869f-2c8e-4986-90c8-ff256d5bc7e0";
    var expectedOrderList = new List<Order>();
    var serverResponse = new SuccessResponse(Messages.GetOrderListSuccess, expectedOrderList);

    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetOrderListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var result = await _orderController.GetOrderList(userId);
    var response = (OkObjectResult) result;

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result);
    Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual((int) HttpStatusCode.OK, ((ServerResponse) response.Value)!.Code);
    Assert.AreEqual(serverResponse, response.Value);
  }

  [Test]
  public async Task GetOrderList_Returns_InternalServerError_When_Exception_Occurs()
  {
    // Arrange
    var userId = "9833869f-2c8e-4986-90c8-ff256d5bc7e0";
    var serverResponse = new InternalServerError("Internal Server Error");

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetOrderListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var result = await _orderController.GetOrderList(userId);
    var response = (ObjectResult) result;

    // Assert
    Assert.IsInstanceOf<ObjectResult>(result);
    Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual((int) HttpStatusCode.InternalServerError, ((InternalServerError) response.Value)!.Code);
    Assert.AreEqual(serverResponse, response.Value);
  }

  [Test]
  public async Task CreateOrder_Returns_OkResult_When_Successful()
  {
    // Arrange
    var testModel = TestModels.TestOrderDto;
    var successResponse = new SuccessResponse("Order created successfully", testModel);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(successResponse);

    // Act
    var result = await _orderController.CreateOrder(testModel);
    var response = result as OkObjectResult;

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result);
    Assert.IsNotNull(response);
    Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual((int) HttpStatusCode.OK, ((ServerResponse) response.Value)!.Code);
    Assert.AreEqual(successResponse, response.Value);
  }

  [Test]
  public async Task CreateOrder_Returns_InternalServerError_When_Exception_Occurs()
  {
    // Arrange
    var testModel = TestModels.TestOrderDto;
    var responseError = new InternalServerError("Internal Server Error");

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(responseError);

    // Act
    var result = await _orderController.CreateOrder(testModel);
    var response = result as ObjectResult;

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result);
    Assert.IsNotNull(response);
    Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual((int) HttpStatusCode.InternalServerError, ((InternalServerError) response.Value)!.Code);
    Assert.AreEqual(responseError, response.Value);
  }
}
