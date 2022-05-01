using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Tests.UnitTests.UseCases;

[TestFixture]
public class DeleteOrderCommandTests
{
  private Mock<IOrderRepository> _orderRepositoryMock;
  private DeleteOrderCommandHandler _handler;

  [SetUp]
  public void SetUp()
  {
    _orderRepositoryMock = new Mock<IOrderRepository>();
    _handler = new DeleteOrderCommandHandler(_orderRepositoryMock.Object);
  }

  [Test]
  public async Task Handle_Valid_OrderId_Deletes_Order()
  {
    // Arrange
    var orderId = TestModels.TestOrderDto.Id;
    var repositoryMock = new Mock<IOrderRepository>();

    repositoryMock
      .Setup(repo => repo.DeleteOrderAsync(orderId))
      .Returns(Task.CompletedTask);

    var handler = new DeleteOrderCommandHandler(repositoryMock.Object);
    var request = new DeleteOrderCommand {OrderId = orderId};

    // Act
    var result = await handler.Handle(request, CancellationToken.None);

    // Assert
    Assert.IsInstanceOf<SuccessResponse>(result);
    Assert.AreEqual(Messages.OrderDeleteSuccess, ((SuccessResponse) result).Message);
    repositoryMock.Verify(repo => repo.DeleteOrderAsync(orderId), Times.Once);
  }

  [Test]
  public async Task Handle_Should_Return_InternalServerError_When_DeleteOrderAsync_Throws_Exception()
  {
    // Arrange
    var orderId = TestModels.TestOrderDto.Id;
    var command = new DeleteOrderCommand {OrderId = orderId};

    // Mock
    _orderRepositoryMock
      .Setup(r => r.DeleteOrderAsync(orderId))
      .ThrowsAsync(new Exception("Test exception"));

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.IsInstanceOf<InternalServerError>(result);
    Assert.AreEqual("DeleteOrderCommand: Test exception", result.Message);
  }
}
