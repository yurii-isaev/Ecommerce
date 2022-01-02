using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Dto;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands
{
  /// <summary>
  /// Sets a property of the request object.
  /// </summary>
  public class RegisterUserCommand : IRequest<Response>
  {
    public RegisterUserDto? RegisterUserDto { get; set; }
  }

  public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response>
  {
    readonly UserManager<User> _userManager;
    readonly IMapper _mapper;

    public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper)
    {
      _userManager = userManager;
      _mapper = mapper;
    }

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Returns ResponseModel.</returns>
    public async Task<Response> Handle(RegisterUserCommand request, CancellationToken token)
    {
      try
      {
        var user = _mapper.Map<User>(request.RegisterUserDto);
        user.DateCreated = DateTime.Now;
        var result = await _userManager.CreateAsync(user, request.RegisterUserDto!.Password);

        if (result.Succeeded)
        {
          var tempUser = await _userManager.FindByEmailAsync(request.RegisterUserDto.Email);
          await _userManager.AddToRoleAsync(tempUser, RoleNameTypes.AllRoles.ElementAt(0));
          return await Task.FromResult(new Response(ResponseCode.Ok, Messages.RegistrationSuccess, ""));
        }

        return await Task.FromResult(new Response(ResponseCode.Ok, Messages.RegistrationFailed,
            result.Errors.Select(e => e.Description).ToArray()
          )
        );
      }
      catch (Exception ex)
      {
        return await Task.FromResult(new Response(ResponseCode.Error, ex.Message, ""));
      }
    }
  }
}
