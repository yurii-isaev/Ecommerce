using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;

namespace WebAPI.Authentication.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
  readonly string _connectionString;

  public OrderRepository(string connectionString)
  {
    _connectionString = connectionString;
  }

  public async Task CreateOrderAsync(Order order)
  {
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
      await connection.OpenAsync();

      using (var transaction = connection.BeginTransaction())
      {
        try
        {
          // Insert the data into the Orders table.
          string insertOrderQuery = @"
             INSERT INTO Orders (Subtotal, Tax, Total, Discount, Quantity, IsPaid)
             OUTPUT INSERTED.Id
             VALUES (@Subtotal, @Tax, @Total, @Discount, @Quantity, @IsPaid)";

          Guid orderId = await connection.ExecuteScalarAsync<Guid>(insertOrderQuery, order, transaction);
          
          string insertOrderItemsQuery = @"
             INSERT INTO OrderItems (OrderId, Article, Avalible, Category, Filling, Image, ImageSlice, Name, Price, Quantity, Tier, Weight)
             VALUES (@OrderId, @Article, @Avalible, @Category, @Filling, @Image, @ImageSlice, @Name, @Price, @Quantity, @Tier, @Weight)";
          
          foreach (var item in order.OrderItems)
          {
            item.OrderId = orderId;
            await connection.ExecuteAsync(insertOrderItemsQuery, item, transaction);
          }

          // Insert the data into the CardPayments table.
          string insertCardPaymentsQuery = @"
            INSERT INTO CardPayments (OrderId, CardHolder, CardNumber, ExpMonth, ExpYear, Cvv, UserId)
            VALUES (@OrderId, @CardHolder, @CardNumber, @ExpMonth, @ExpYear, @Cvv, @UserId)";
      
          // Set the OrderId for the CardPayment.
          order.CardPayment.OrderId = orderId;

          await connection.ExecuteAsync(insertCardPaymentsQuery, order.CardPayment, transaction);
          transaction.Commit();
        }
        catch (Exception)
        {
          transaction.Rollback();
          throw;
        }
      }
    }
  }
}
