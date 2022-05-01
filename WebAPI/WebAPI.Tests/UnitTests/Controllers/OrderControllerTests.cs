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
    var userId = TestModels.TestOrderDto.UserId;
    var expectedOrderList = new List<Order>();
    var serverResponse = new SuccessResponse(Messages.GetOrderListSuccess, expectedOrderList);

    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetOrderListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _orderController.GetOrderList(userId);

    // Assert
    var okResult = response as ObjectResult;
    Assert.NotNull(okResult);
    Assert.AreEqual((int) HttpStatusCode.OK, okResult.StatusCode);
    Assert.AreEqual(serverResponse, okResult!.Value);

    var responseData = okResult.Value as ServerResponse;
    Assert.NotNull(responseData);
    Assert.AreEqual((int) HttpStatusCode.OK, responseData.Code);
    Assert.AreEqual(Messages.GetOrderListSuccess, responseData.Message);
    Assert.NotNull(responseData.Set);
  }

  [Test]
  public async Task GetOrderList_Returns_InternalServerError_When_Exception_Occurs()
  {
    // Arrange
    var userId = TestModels.TestOrderDto.UserId;
    var serverResponse = new InternalServerError(Messages.ServerError);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetOrderListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _orderController.GetOrderList(userId);

    // Assert
    var errorResult = response as ObjectResult;
    Assert.NotNull(errorResult);
    Assert.AreEqual((int) HttpStatusCode.OK, errorResult.StatusCode);
    Assert.AreEqual(serverResponse, errorResult!.Value);

    var responseData = errorResult!.Value as ServerResponse;
    Assert.NotNull(serverResponse);
    Assert.AreEqual((int) HttpStatusCode.InternalServerError, responseData.Code);
    Assert.AreEqual(serverResponse, errorResult.Value);
  }

  [Test]
  public async Task CreateOrder_Returns_OkResult_When_Successful()
  {
    // Arrange
    var testModel = TestModels.TestOrderDto;
    var serverResponse = new SuccessResponse(Messages.OrderCreatedSuccess, testModel);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _orderController.CreateOrder(testModel);

    // Assert
    var okResult = response as ObjectResult;
    Assert.NotNull(okResult);
    Assert.AreEqual((int) HttpStatusCode.OK, okResult.StatusCode);
    Assert.AreEqual(serverResponse, okResult!.Value);

    var responseData = okResult.Value as ServerResponse;
    Assert.NotNull(responseData);
    Assert.AreEqual((int) HttpStatusCode.OK, responseData.Code);
    Assert.AreEqual(Messages.OrderCreatedSuccess, responseData.Message);
    Assert.NotNull(responseData.Set);
  }

  [Test]
  public async Task CreateOrder_Returns_InternalServerError_When_Exception_Occurs()
  {
    // Arrange
    var testModel = TestModels.TestOrderDto;
    var serverResponse = new InternalServerError(Messages.ServerError);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _orderController.CreateOrder(testModel);

    // Assert
    var errorResult = response as ObjectResult;
    Assert.NotNull(errorResult);
    Assert.AreEqual((int) HttpStatusCode.OK, errorResult.StatusCode);
    Assert.AreEqual(serverResponse, errorResult!.Value);

    var responseData = errorResult!.Value as InternalServerError;
    Assert.NotNull(response);
    Assert.AreEqual((int) HttpStatusCode.InternalServerError, responseData.Code);
    Assert.AreEqual(serverResponse, errorResult.Value);
  }
}
