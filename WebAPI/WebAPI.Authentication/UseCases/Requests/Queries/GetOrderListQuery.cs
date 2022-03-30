using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Queries;

public class GetOrderListQuery : IRequest<ServerResponse>
{
  public string UserId { get; set; } = null!;
}

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, ServerResponse>
{
  readonly IOrderRepository _repository;

  public GetOrderListQueryHandler(IOrderRepository repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Handles a request.
  /// </summary>
  /// <param name="request">The request.</param>
  /// <param name="token">Cancellation token.</param>
  /// <returns>Returns user order list.</returns>
  public async Task<ServerResponse> Handle(GetOrderListQuery request, CancellationToken token)
  {
    try
    {
      var userOrderList = await _repository.GetOrdersByUserId(request.UserId);
      return new SuccessResponse(Messages.GetOrderListSuccess, userOrderList);
    }
    catch (Exception e)
    {
      return new InternalServerError("GetOrderListQuery: " + e.Message);
    }
  }
}
