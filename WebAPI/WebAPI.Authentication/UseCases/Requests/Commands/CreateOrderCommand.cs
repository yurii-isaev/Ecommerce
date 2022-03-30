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

public class CreateOrderCommand: IRequest<ServerResponse>
{
  public OrderDto OrderDto { get; set; } = null!;
}


public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServerResponse>
{
  readonly IOrderRepository _repository;
  readonly IMapper _mapper;

  public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ServerResponse> Handle(CreateOrderCommand request, CancellationToken token)
  {
    try
    {
      var order = _mapper.Map<Order>(request.OrderDto);
      await _repository.CreateOrderAsync(order);
      return new SuccessResponse(Messages.OrderCreatedSuccess, null);
    }
    catch (Exception e)
    {
      return new InternalServerError("OrderCommand: " + e.Message);
    }
  }
}