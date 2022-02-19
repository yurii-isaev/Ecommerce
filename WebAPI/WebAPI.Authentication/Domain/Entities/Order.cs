using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Authentication.Domain.Entities;

public class Order
{
  // [Key]
  // public Guid Id { get; set; }
  
  public decimal Subtotal { get; set; }
  public decimal Tax { get; set; }
  public decimal Total { get; set; }
  public decimal Discount { get; set; }
  public int Quantity { get; set; }
  public bool IsPaid { get; set; }

  // Navigation properties
  public List<OrderItem> OrderItems { get; set; } = null!;
  
  public CardPayment CardPayment { get; set; } = null!;
}

public class OrderItem
{
  // [Key]
  // public Guid Id { get; set; }
  
  [ForeignKey("OrderId")]
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = null!;

  // 
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
}

public class Layer
{
  public string Type { get; set; } = null!;

  public int Height { get; set; }
}

public class CardPayment
{
  // [Key]
  // public Guid Id { get; set; }

  [ForeignKey("OrderId")]
  public Guid? OrderId { get; set; } = null!;
  public Order Order { get; set; } = null!;

  //
  public string CardHolder { get; set; } = null!;
  public string CardNumber { get; set; } = null!;
  public string ExpMonth { get; set; } = null!;
  public int ExpYear { get; set; }
  public string Cvv { get; set; } = null!;
  public string UserId { get; set; } = null!;
}
