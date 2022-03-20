using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Queries;

public class GetUserOrderListQuery : IRequest<ServerResponse>
{
  public string UserId { get; set; } = null!;
}

public class GetUserOrderListQueryHandler : IRequestHandler<GetUserOrderListQuery, ServerResponse>
{
  readonly IOrderRepository _repository;

  public GetUserOrderListQueryHandler(IOrderRepository repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Handles a request.
  /// </summary>
  /// <param name="request">The request.</param>
  /// <param name="token">Cancellation token.</param>
  /// <returns>Returns user order list.</returns>
  public async Task<ServerResponse> Handle(GetUserOrderListQuery request, CancellationToken token)
  {
    try
    {
      var userOrderList = await _repository.GetOrdersByUserId(request.UserId);
      return new SuccessResponse(Messages.GetOrderListSuccess, userOrderList);
    }
    catch (Exception e)
    {
      return new InternalServerError("GetUserOrderListQuery: " + e.Message);
    }
  }
}
