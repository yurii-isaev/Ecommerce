using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;

namespace WebAPI.Authentication.UseCases.Requests.Queries
{
  public class GetProductsListQuery : IRequest<IEnumerable>
  {}

  public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IEnumerable>
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
    /// <returns>Returns employee list.</returns>
    public async Task<IEnumerable> Handle(GetProductsListQuery request, CancellationToken token)
    {
      IEnumerable<Product> productList = await _repository.GetProductList();
      return productList;
    }
  }
}
