using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Authentication.Controllers;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Tests.UnitTests.Controllers;

[TestFixture]
public class StoreControllerTests
{
  private Mock<IMediator> _mockMediator;
  private StoreController _controller;

  [SetUp]
  public void Setup()
  {
    _mockMediator = new Mock<IMediator>();
    _controller = new StoreController(_mockMediator.Object);
  }

  [Test]
  public async Task GetAllProducts_When_Query_Returns_Ok_With_Products()
  {
    // Arrange
    var pageNumber = 1;
    var pageSize = 10;
    var mockProducts = new List<Product>
    {
      new() {Id = new Guid(), Name = "Test Product 1"},
      new() {Id = new Guid(), Name = "Test Product 2"}
    };

    var dataSet = new {items = mockProducts, totalPages = 5};
    var serverResponse = new SuccessResponse(Messages.GetProductListSuccess, dataSet);
    
    // Mock
    _mockMediator
      .Setup(m => m.Send(It.IsAny<GetProductsListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var result = await _controller.GetAllProducts(pageNumber, pageSize);
    var okResult = result as OkObjectResult;
    var realResponse = okResult!.Value as SuccessResponse;
    var realResponseSet = realResponse!.Set;

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result);
    Assert.IsNotNull(okResult);
    Assert.IsNotNull(realResponse);
    Assert.IsNotNull(realResponseSet);
    Assert.AreEqual(dataSet, realResponseSet);
  }
  
  [Test]
  public async Task GetAllProducts_When_Query_Returns_InternalServerError()
  {
    // Arrange
    var pageNumber = 1;
    var pageSize = 10;
    var exception = new InternalServerError("Internal Server Error");

    // Mock
    _mockMediator
      .Setup(m => m.Send(It.IsAny<GetProductsListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(exception);

    // Act
    var result = await _controller.GetAllProducts(pageNumber, pageSize);
    var objectResult = result as ObjectResult;
    var serverResponse = objectResult!.Value as ServerResponse;

    // Assert
    Assert.IsInstanceOf<ObjectResult>(result);
    Assert.IsNotNull(objectResult);
    Assert.AreEqual(exception, objectResult.Value);

    Assert.IsNotNull(serverResponse);
    Assert.IsFalse(serverResponse.Success);
    Assert.AreEqual(exception.Message, serverResponse.Message);
  }
}
