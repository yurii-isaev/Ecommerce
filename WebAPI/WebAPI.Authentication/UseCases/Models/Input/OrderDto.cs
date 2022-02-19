using System.Collections.Generic;

namespace WebAPI.Authentication.UseCases.Models.Input;

public class OrderDto
{
  public decimal Subtotal { get; set; }

  public decimal Tax { get; set; }

  public decimal Total { get; set; }

  public decimal Discount { get; set; }

  public int Quantity { get; set; }
  
  public bool IsPaid { get; set; }

  public List<OrderItemDto> OrderItems { get; set; } = new();
  
  public CardPaymentDto  CardPayment { get; set; } = new();
}

public class OrderItemDto
{
  public string Article { get; set; } = null!;

  public bool Avalible { get; set; }

  public string Category { get; set; } = null!;

  public string Filling { get; set; } = null!;

  public string Image { get; set; } = null!;

  public string ImageSlice { get; set; } = null!;
  
  public LayerDto[]? Layers { get; set; }

  public string Name { get; set; } = null!;

  public decimal Price { get; set; }

  public int Quantity { get; set; }

  public int Tier { get; set; }

  public int Weight { get; set; }
}

public class LayerDto
{
  public string Type { get; set; } = null!;

  public int Height { get; set; }
}

public class CardPaymentDto 
{
  public string CardHolder { get; set; } = null!;

  public string CardNumber { get; set; } = null!;

  public string ExpMonth { get; set; } = null!;

  public int ExpYear { get; set; }

  public string Cvv { get; set; } = null!;

  public string UserId { get; set; } = null!;
}
