using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Queries;

namespace WebAPI.Tests.UnitTests.UseCases;

[TestFixture]
public class GetProductsListQueryTests
{
  private Mock<IProductRepository> _mockRepository;
  private GetProductsListQueryHandler _handler;

  [SetUp]
  public void Setup()
  {
    _mockRepository = new Mock<IProductRepository>();
    _handler = new GetProductsListQueryHandler(_mockRepository.Object);
  }

  [Test]
  public async Task Handle_Should_Return_InternalServerError_When_Database_Error_Occurs()
  {
    // Arrange
    var pageNumber = 1;
    var pageSize = 10;
    var request = new GetProductsListQuery {PageNumber = pageNumber, PageSize = pageSize};
    var cancellationToken = new CancellationToken();
    var databaseError = new Exception("Database error");

    // Mock
    _mockRepository
      .Setup(repo => repo.GetProductList(It.IsAny<int>(), It.IsAny<int>()))
      .ThrowsAsync(databaseError);

    // Act
    var result = await _handler.Handle(request, cancellationToken);

    // Assert
    Assert.IsInstanceOf<InternalServerError>(result);
    Assert.AreEqual(StatusCodes.Status500InternalServerError, result.Code);
    Assert.AreEqual($"GetProductsListQuery: {databaseError.Message}", result.Message);
  }
}
