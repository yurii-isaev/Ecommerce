using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Authentication.Domain.Entities;

namespace WebAPI.Authentication.UseCases.Contracts;

public interface IProductRepository
{
  Task<(List<Product> products, int totalProducts)> GetProductList(int requestPageNumber, int requestPageSize);
}
