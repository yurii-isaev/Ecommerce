using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Authentication.Controllers;
using WebAPI.Authentication.UseCases.Models;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Requests.Commands;
using WebAPI.Authentication.UseCases.Requests.Queries;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Tests.UnitTests.Controllers;

[TestFixture]
public class AuthControllerTests
{
  private Mock<IMediator> _mediatorMock;
  private AuthController _controller;

  [SetUp]
  public void SetUp()
  {
    _mediatorMock = new Mock<IMediator>();
    _controller = new AuthController(_mediatorMock.Object);
  }

  [Test]
  public async Task SignUp_Should_Return_OkResult_When_Registration_Succeeds()
  {
    // Arrange
    var registerDto = TestModels.TestRegisterDto;
    var serverResponse = new SuccessResponse(Messages.RegistrationSuccess, registerDto);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _controller.SignUp(registerDto);

    // Assert
    var okResult = response as ObjectResult;
    Assert.NotNull(okResult);
    Assert.AreEqual((int) HttpStatusCode.OK, okResult.StatusCode);
    Assert.AreEqual(serverResponse, okResult!.Value);
    
    var responseData = okResult.Value as ServerResponse;
    Assert.NotNull(responseData);
    Assert.NotNull(responseData.Set);
    Assert.AreEqual((int) HttpStatusCode.OK, responseData.Code);
    Assert.AreEqual(Messages.RegistrationSuccess, responseData.Message);
  }

  [Test]
  public async Task SignUp_Should_Return_BadRequest_When_Registration_Fails()
  {
    // Arrange
    var registerDto = TestModels.TestRegisterDto;
    var serverResponse = new InternalServerError(Messages.RegistrationFailed);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(serverResponse);

    // Act
    var response = await _controller.SignUp(registerDto);

    // Assert
    var okResult = response as ObjectResult;
    Assert.NotNull(okResult);
    Assert.AreEqual((int) HttpStatusCode.OK, okResult.StatusCode);
    Assert.AreEqual(serverResponse, okResult!.Value);

    var responseData = okResult.Value as InternalServerError;
    Assert.NotNull(responseData);
    Assert.AreEqual((int) HttpStatusCode.InternalServerError, responseData.Code);
    Assert.AreEqual(Messages.RegistrationFailed, responseData.Message);
  }

  [Test]
  public async Task GetAuthProfile_Returns_OkObjectResult_With_Profile()
  {
    // Arrange
    var expectedProfile = TestModels.TestProfileDto;
    var successResponse = new SuccessResponse(Messages.AuthSuccess, new {profile = expectedProfile});

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetAuthProfileQuery>(), CancellationToken.None))
      .ReturnsAsync(successResponse);

    // Act
    var response = await _controller.GetAuthProfile();

    // Assert
    var okResult = response as OkObjectResult;
    Assert.NotNull(okResult);
    Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

    var responseData = okResult.Value as SuccessResponse;
    Assert.NotNull(responseData);
    Assert.IsInstanceOf<ProfileDto>(responseData.Set.profile);

    var profile = responseData.Set.profile as ProfileDto;
    Assert.AreEqual(expectedProfile.Id, profile.Id);
    Assert.AreEqual(expectedProfile.UserName, profile.UserName);
    Assert.AreEqual(expectedProfile.Email, profile.Email);
    Assert.AreEqual(expectedProfile.Role, profile.Role);
  }

  [Test]
  public async Task GetAuthProfile_Returns_InternalServerError_When_Exception_Occurs()
  {
    // Arrange
    var errorMessage = "internal Server Error";
    var errorResponse = new InternalServerError(Messages.ServerError + errorMessage);

    // Mock
    _mediatorMock
      .Setup(m => m.Send(It.IsAny<GetAuthProfileQuery>(), CancellationToken.None))
      .ReturnsAsync(errorResponse);

    // Act
    var response = await _controller.GetAuthProfile();

    // Assert
    var objectResult = response as ObjectResult;
    Assert.NotNull(objectResult);
    Assert.AreEqual((int) HttpStatusCode.OK, objectResult.StatusCode);

    var responseData = objectResult.Value as InternalServerError;
    Assert.NotNull(responseData);
    Assert.AreEqual((int) HttpStatusCode.InternalServerError, responseData.Code);
    Assert.AreEqual(Messages.ServerError + errorMessage, responseData.Message);
  }
}
