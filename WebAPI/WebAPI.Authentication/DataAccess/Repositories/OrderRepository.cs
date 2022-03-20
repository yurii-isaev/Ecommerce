using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using WebAPI.Authentication.Domain.Entities;
using WebAPI.Authentication.UseCases.Contracts;
using WebAPI.Authentication.UseCases.Models.Input;

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
            INSERT INTO Orders (Subtotal, Tax, Total, Discount, Quantity, IsPaid, UserId)
            OUTPUT INSERTED.Id
            VALUES (@Subtotal, @Tax, @Total, @Discount, @Quantity, @IsPaid, @UserId)";

          Guid orderId = await connection.ExecuteScalarAsync<Guid>(insertOrderQuery, order, transaction);

          string insertOrderDetailsQuery = @"
            INSERT INTO OrderDetails (OrderId, Article, Avalible, Category, Filling, Image, ImageSlice, Name, Price, Quantity, Tier, Weight)
            VALUES (@OrderId, @Article, @Avalible, @Category, @Filling, @Image, @ImageSlice, @Name, @Price, @Quantity, @Tier, @Weight)";
          
          foreach (var item in order.OrderDetails)
          {
            item.OrderId = orderId;
            await connection.ExecuteAsync(insertOrderDetailsQuery, item, transaction);
          }

          // Insert the data into the OrderCardPayments table.
          string insertOrderPaymentsQuery = @"
            INSERT INTO OrderCardPayments (OrderId, CardHolder, CardNumber, ExpMonth, ExpYear, Cvv, UserId)
            VALUES (@OrderId, @CardHolder, @CardNumber, @ExpMonth, @ExpYear, @Cvv, @UserId)";

          // Set the OrderId for the OrderCardPayments table.
          order.OrderCardPayment.OrderId = orderId;
          await connection.ExecuteAsync(insertOrderPaymentsQuery, order.OrderCardPayment, transaction);

          if (order.OrderAddress != null)
          {
            string insertOrderAddressQuery = @"
              INSERT INTO OrderAddress (OrderId, FullName, Address, City, State, ZipCode, ConsentPrivateData)
              VALUES (@OrderId, @FullName, @Address, @City, @State, @ZipCode, @ConsentPrivateData)";

            order.OrderAddress.OrderId = orderId;
            await connection.ExecuteAsync(insertOrderAddressQuery, order.OrderAddress, transaction);
          }

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

  public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
  {
    var query = @"
        SELECT o.*, od.*, oa.*, op.* 
        FROM Orders o
        LEFT JOIN OrderDetails od ON o.Id = od.OrderId
        LEFT JOIN OrderAddress oa ON o.Id = oa.OrderId
        LEFT JOIN OrderCardPayments op ON o.Id = op.OrderId
        WHERE o.UserId = @UserId
        ORDER BY o.Id";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
      await connection.OpenAsync();

      var ordersDictionary = new Dictionary<Guid, Order>();

      var results = await connection.QueryAsync<Order, OrderDetails, OrderAddress, OrderCardPayment, Order>(
        query,
        (order, orderDetails, orderAddress, orderPayment) =>
        {
          if (!ordersDictionary.TryGetValue(order.Id, out var currentOrder))
          {
            currentOrder = order;
            currentOrder.OrderDetails = new List<OrderDetails>();
            ordersDictionary.Add(order.Id, currentOrder);
          }

          if (orderDetails != null)
          {
            currentOrder.OrderDetails.Add(orderDetails);
          }

          if (orderAddress != null)
          {
            currentOrder.OrderAddress = orderAddress;
          }

          if (orderPayment != null)
          {
            currentOrder.OrderCardPayment = orderPayment;
          }

          return currentOrder;
        },
        new { UserId = userId },
        splitOn: "Id,Id,Id,Id"
      );

      return ordersDictionary.Values;
    }
  }
}
