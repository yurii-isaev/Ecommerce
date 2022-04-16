using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Tests.UnitTests.UseCases;

[TestFixture]
public class CreateOrderCommandTests
{
  private Mock<IOrderRepository> _repositoryMock;
  private Mock<IMapper> _mapperMock;
  private CreateOrderCommandHandler _handler;

  [SetUp]
  public void Setup()
  {
    _repositoryMock = new Mock<IOrderRepository>();
    _mapperMock = new Mock<IMapper>();
    _handler = new CreateOrderCommandHandler(_repositoryMock.Object, _mapperMock.Object);
  }

  [Test]
  public async Task Handle_Returns_SuccessResponse_With_Valid_Order()
  {
    // Arrange
    var orderDto = new OrderDto();
    var order = new Order();
    var serverResponse = new SuccessResponse(Messages.OrderCreatedSuccess, null);

    _mapperMock
      .Setup(m => m.Map<Order>(It.IsAny<OrderDto>()))
      .Returns(order);

    _repositoryMock
      .Setup(r => r.CreateOrderAsync(It.IsAny<Order>()))
      .Returns(Task.CompletedTask);

    // Act
    var result = await _handler.Handle(new CreateOrderCommand {OrderDto = orderDto}, CancellationToken.None);
    var successResponse = (SuccessResponse) result;

    // Assert
    Assert.IsInstanceOf<SuccessResponse>(result);
    Assert.AreEqual(serverResponse.Message, successResponse.Message);
    Assert.IsNull(successResponse.Set);
  }

  [Test]
  public async Task Handle_Returns_InternalServerError_When_Exception_Occurs()
  {
    // Arrange
    var orderDto = new OrderDto();
    var exceptionMessage = "An error occurred while creating the order.";
    var serverResponse = new InternalServerError("OrderCommand: " + exceptionMessage);

    _mapperMock
      .Setup(m => m.Map<Order>(It.IsAny<OrderDto>()))
      .Throws(new Exception(exceptionMessage));

    // Act
    var result = await _handler.Handle(new CreateOrderCommand {OrderDto = orderDto}, CancellationToken.None);
    var errorResponse = (InternalServerError) result;

    // Assert
    Assert.IsInstanceOf<InternalServerError>(result);
    Assert.AreEqual(serverResponse.Message, errorResponse.Message);
  }
}
