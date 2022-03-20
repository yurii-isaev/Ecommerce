using System.Collections.Generic;

namespace WebAPI.Authentication.UseCases.Models.Input;

public class OrderDto
{
  public decimal Subtotal { get; set; }

  public decimal Tax { get; set; }

  public decimal Total { get; set; }

  public decimal Discount { get; set; }

  public int Quantity { get; set; }
  
  public string UserId { get; set; } = null!;

  public bool IsPaid { get; set; }

  public List<OrderDetailsDto> OrderDetails { get; set; } = new();

  public OrderCardPaymentDto OrderCardPayment { get; set; } = new();
  
  // Optional property
  public OrderAddressDto? OrderAddress { get; set; } 
}

public class OrderAddressDto
{
  public string FullName { get; set; } = null!;

  public string Address { get; set; } = null!;

  public string City { get; set; } = null!;

  public string State { get; set; } = null!;

  public string ZipCode { get; set; } = null!;

  public bool ConsentPrivateData { get; set; }
}

public class OrderDetailsDto
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

public class OrderCardPaymentDto
{
  public string CardHolder { get; set; } = null!;

  public string CardNumber { get; set; } = null!;

  public string ExpMonth { get; set; } = null!;

  public int ExpYear { get; set; }

  public string Cvv { get; set; } = null!;

  public string UserId { get; set; } = null!;
}
