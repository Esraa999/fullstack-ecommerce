using ECommerce.API.Models;
using ECommerce.API.Services;
using ECommerce.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _svc;
    public ProductsController(IProductService svc) { _svc = svc; }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var p = await _svc.GetByIdAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
    {
        var product = new Product
        {
            Category = dto.Category,
            ProductCode = dto.ProductCode,
            Name = dto.Name,
            Price = dto.Price,
            MinimumQuantity = dto.MinimumQuantity,
            DiscountRate = dto.DiscountRate
        };

        if (dto.Image != null)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid()}_{dto.Image.FileName}";
            var path = Path.Combine(folder, fileName);
            using var stream = System.IO.File.Create(path);
            await dto.Image.CopyToAsync(stream);
            product.ImagePath = Path.Combine("UploadedImages", fileName);
        }

        var created = await _svc.CreateAsync(product);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }
}
