using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Output;
using WebAPI.Authentication.UseCases.Types;

namespace WebAPI.Authentication.UseCases.Requests.Queries;

public class GetProductsListQuery : IRequest<ServerResponse>
{
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
}

public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, ServerResponse>
{
  readonly IProductRepository _repository;

  public GetProductsListQueryHandler(IProductRepository repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Handles a request.
  /// </summary>
  /// <param name="request">The request.</param>
  /// <param name="token">Cancellation token.</param>
  /// <returns>Returns product list.</returns>
  public async Task<ServerResponse> Handle(GetProductsListQuery request, CancellationToken token)
  {
    (List<Product> products, int totalProducts) tuple;
    try
    {
      tuple = await _repository.GetProductList(request.PageNumber, request.PageSize);

      var products = tuple.products;
      var totalProducts = tuple.totalProducts;
      var totalPagesNumber = (int) Math.Ceiling((double) totalProducts / request.PageSize);
      var data = new {items = products, totalPages = totalPagesNumber};

      return new SuccessResponse(Messages.GetProductListSuccess, data);
    }
    catch (Exception e)
    {
      return new InternalServerError("GetProductsListQuery: " + e.Message);
    }
  }
}
