using System;

namespace WebAPI.Authentication.Domain.Entities;

public class Product
{
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Image { get; set; }
  public string? ImageSlice { get; set; }
  public string? Article { get; set; }
  public bool Avalible { get; set; }
  public string? Category { get; set; }
  public string? Filling { get; set; }
  public int Weight { get; set; }
  public int Tier { get; set; }
  public decimal Price { get; set; }
}
