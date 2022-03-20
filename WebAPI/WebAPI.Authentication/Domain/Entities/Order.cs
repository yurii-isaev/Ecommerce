using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Authentication.Domain.Entities;

public class Order
{
  [Key]
  public Guid Id { get; set; }
  //
  public decimal Subtotal { get; set; }
  public decimal Tax { get; set; }
  public decimal Total { get; set; }
  public decimal Discount { get; set; }
  public int Quantity { get; set; }
  public bool IsPaid { get; set; }
  public DateTime CreatedAt { get; set; }

  //
  [ForeignKey("UserId")]
  public string UserId { get; set; } = null!;

  // Navigation properties
  public OrderAddress? OrderAddress { get; set; }
  
  public List<OrderDetails> OrderDetails { get; set; } = null!;
  
  public OrderCardPayment OrderCardPayment { get; set; } = null!;
}

public class OrderAddress
{
  [Key]
  public Guid Id { get; set; }
  
  public string FullName { get; set; } = null!;
  public string Address { get; set; } = null!;
  public string City { get; set; } = null!;
  public string State { get; set; } = null!;
  public string ZipCode { get; set; } = null!;
  public bool ConsentPrivateData { get; set; }
  //
  [ForeignKey("OrderId")]
  public Guid OrderId { get; set; } 
  public Order Order { get; set; } = null!;
}

public class OrderDetails
{
  [Key]
  public Guid Id { get; set; }
  
  public string Article { get; set; } = null!;
  public bool Avalible { get; set; }
  public string Category { get; set; } = null!;
  public string Filling { get; set; } = null!;
  public string Image { get; set; } = null!;
  public string ImageSlice { get; set; } = null!;
  public Layer[]? Layers { get; set; }
  public string Name { get; set; } = null!;
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public int Tier { get; set; }
  public int Weight { get; set; }
  //
  [ForeignKey("OrderId")]
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = null!;
}

public class Layer
{
  public string Type { get; set; } = null!;
  public int Height { get; set; }
}

public class OrderCardPayment
{
  [Key]
  public Guid Id { get; set; }
  
  public string CardHolder { get; set; } = null!;
  public string CardNumber { get; set; } = null!;
  public string ExpMonth { get; set; } = null!;
  public int ExpYear { get; set; }
  public string Cvv { get; set; } = null!;
  //
  [ForeignKey("OrderId")]
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = null!;
  //
  [ForeignKey("UserId")]
  public string UserId { get; set; } = null!;
}
