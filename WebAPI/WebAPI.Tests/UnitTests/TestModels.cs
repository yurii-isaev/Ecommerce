using System;
using System.Collections.Generic;
using WebAPI.Authentication.UseCases.Models;
using WebAPI.Authentication.UseCases.Models.Input;

namespace WebAPI.Tests.UnitTests;

public class TestModels
{
  public static readonly OrderDto TestOrderDto = new()
  {
    Id = "5B5A1E98-BC38-4387-AFCF-AB8B0125A3AC",
    Subtotal = 200.23412341m,
    Tax = 0m,
    Total = 200.23412341m,
    Discount = 0m,
    Quantity = 1,
    UserId = "9833869f-2c8e-4986-90c8-ff256d5bc7e0",
    IsPaid = true,
    OrderAddress = null,
    OrderDetails = new List<OrderDetailsDto>
    {
      new OrderDetailsDto
      {
        Article = "000405",
        Avalible = true,
        Category = "cake",
        Filling = "fruits",
        Image = "tired_pear_cake.jpg",
        ImageSlice = "tired_pear_cake_slice.jpg",
        Name = "Tired Pear Cake",
        Price = 200.23412341m,
        Quantity = 1,
        Tier = 1,
        Weight = 1400
      }
    },
    OrderCardPayment = new OrderCardPaymentDto
    {
      CardHolder = "Test",
      CardNumber = "1111222233334444",
      ExpMonth = "12",
      ExpYear = 2027,
      Cvv = "261",
      UserId = "9833869f-2c8e-4986-90c8-ff256d5bc7e0"
    }
  };

  public static readonly RegisterDto TestRegisterDto = new()
  {
    UserName = "TestUser",
    Email = "test@example.com",
    Password = "8V4-2wE-5tk-qKL"
  };
  
  public static readonly ProfileDto TestProfileDto = new()
  {
    Id = "a70d0fe0-1ab4-4b07-a90c-1c270481bf7b",
    Email = "test@example.com",
    UserName = "TestUser",
    DateCreated = new DateTime(),
    Role = "Customer"
  };
}
