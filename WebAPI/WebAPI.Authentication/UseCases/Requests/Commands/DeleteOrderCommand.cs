using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands;

public class DeleteOrderCommand : IRequest<ServerResponse>
{
  public string OrderId { get; set; } = null!;
}


public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ServerResponse>
{
  readonly IOrderRepository _repository;

  public DeleteOrderCommandHandler(IOrderRepository repository)
  {
    _repository = repository;
  }

  public async Task<ServerResponse> Handle(DeleteOrderCommand request, CancellationToken token)
  {
    try
    {
      await _repository.DeleteOrderAsync(request.OrderId);
      return new SuccessResponse(Messages.OrderDeleteSuccess, null);
    }
    catch (Exception e)
    {
      return new InternalServerError("DeleteOrderCommand: " + e.Message);
    }
  }
}