using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Authentication.Domain.Entities;

namespace WebAPI.Authentication.UseCases.Contracts
{
  public interface IProductRepository
  {
    Task CreateProductAsync(Product product);
    
    Task<IEnumerable<Product>> GetProductList();
    
    Task<Product> GetProductAsync(Guid productId);
    
    Task DeleteProductAsync(Guid productId);
  }
}