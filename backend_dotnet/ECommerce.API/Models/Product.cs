using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Models;

public class Product
{
    public int Id { get; set; }
    public string Category { get; set; } = null!;
    public string ProductCode { get; set; } = null!; // unique P01...
    public string Name { get; set; } = null!;
    public string? ImagePath { get; set; } // saved to local storage
    public decimal Price { get; set; }
    public int MinimumQuantity { get; set; }
    public double DiscountRate { get; set; } // percentage, e.g., 10.5
}
