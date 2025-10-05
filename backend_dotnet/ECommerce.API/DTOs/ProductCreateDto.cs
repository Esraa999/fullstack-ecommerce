using Microsoft.AspNetCore.Http;

namespace ECommerce.API.DTOs;
public class ProductCreateDto
{
    public string Category { get; set; } = null!;
    public string ProductCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public IFormFile? Image { get; set; }
    public decimal Price { get; set; }
    public int MinimumQuantity { get; set; }
    public double DiscountRate { get; set; }
}
