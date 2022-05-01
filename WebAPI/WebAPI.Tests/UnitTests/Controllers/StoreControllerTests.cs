using System;
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
    var response = await _controller.GetAllProducts(pageNumber, pageSize);
    
    // Assert
    var okResult = response as ObjectResult;
    Assert.NotNull(okResult);
    Assert.AreEqual((int) HttpStatusCode.OK, okResult.StatusCode);

    var responseData = okResult.Value as SuccessResponse;
    Assert.NotNull(responseData);
    Assert.IsNotNull(responseData.Set);
    Assert.AreEqual((int) HttpStatusCode.OK, responseData.Code);
    Assert.AreEqual(Messages.GetProductListSuccess, responseData.Message);
  }
  
  [Test]
  public async Task GetAllProducts_When_Query_Returns_InternalServerError()
  {
    // Arrange
    var pageNumber = 1;
    var pageSize = 10;
    var serverResponse = new InternalServerError(Messages.ServerError);

    // Mock
    _mockMediator
      .Setup(m => m.Send(It.IsAny<GetProductsListQuery>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _controller.GetAllProducts(pageNumber, pageSize);

    // Assert
    var errorResult = response as ObjectResult;
    Assert.IsNotNull(errorResult);
    Assert.AreEqual(serverResponse, errorResult.Value);

    var responseData = errorResult!.Value as ServerResponse;
    Assert.IsNotNull(responseData);
    Assert.IsFalse(responseData.Success);
    Assert.AreEqual(serverResponse.Message, responseData.Message);
  }
}
