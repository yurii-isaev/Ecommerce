using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;

namespace WebAPI.Authentication.DataAccess.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly string _connection;

    public ProductRepository(string connectionString)
    {
      _connection = connectionString;
    }

    public Task<IEnumerable<Product>> GetProductList()
    {
      using (IDbConnection db = new SqlConnection(_connection))
      {
        string query = "SELECT * FROM Products";
        return Task.FromResult<IEnumerable<Product>>(db.Query<Product>(query).AsList());
      }
    }


    // public async Task CreateProductAsync(Product product)
    // {
    //   throw new NotImplementedException();
    // }
    //
    // public async Task<IEnumerable<Product>> GetProductList()
    // {
    //   throw new NotImplementedException();
    // }
    //
    // public async Task<Product> GetProductAsync(Guid productId)
    // {
    //   throw new NotImplementedException();
    // }
    //
    // public async Task DeleteProductAsync(Guid productId)
    // {
    //   throw new NotImplementedException();
    // }
  }
}
