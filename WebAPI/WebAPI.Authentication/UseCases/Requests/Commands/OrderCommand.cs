using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Input;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Commands;

public class OrderCommand: IRequest<ServerResponse>
{
  public OrderDto OrderDto { get; set; } = null!;
}


public class OrderCommandHandler : IRequestHandler<OrderCommand, ServerResponse>
{
  readonly IOrderRepository _repository;
  readonly IMapper _mapper;

  public OrderCommandHandler(IOrderRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ServerResponse> Handle(OrderCommand request, CancellationToken token)
  {
    try
    {
      var order = _mapper.Map<Order>(request.OrderDto);
      await _repository.CreateOrderAsync(order);
      return await Task.FromResult(new ServerResponse(200, true, Messages.OrderCreatedSuccess, ""));
    }
    catch (Exception e)
    {
      return new InternalServerError(Messages.ServerError + e.Message);
    }
  }
}