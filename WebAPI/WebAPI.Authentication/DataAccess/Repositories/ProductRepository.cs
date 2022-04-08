using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;

namespace WebAPI.Authentication.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
  readonly string _connection;

  public ProductRepository(string connectionString)
  {
    _connection = connectionString;
  }

  public Task<(List<Product> products, int totalProducts)> GetProductList(int pageNumber, int pageSize)

  {
    using (IDbConnection db = new SqlConnection(_connection))
    {
      int skip = (pageNumber - 1) * pageSize;
      var query = $@"SELECT * FROM Products ORDER BY Id OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";
      var products = db.Query<Product>(query).AsList();

      var totalQuery = "SELECT COUNT(*) FROM Products";
      var totalProducts = db.Query<int>(totalQuery).Single();

      return Task.FromResult((products, totalProducts));
    }
  }
}
