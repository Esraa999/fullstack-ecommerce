using ECommerce.API.Data;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;
    public ProductService(AppDbContext db) { _db = db; }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _db.Products.ToListAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FindAsync(id);
    public async Task<Product> CreateAsync(Product p)
    {
        _db.Products.Add(p);
        await _db.SaveChangesAsync();
        return p;
    }
}
