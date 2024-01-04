using ApiProducts.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProducts.Context;

public class ApiProductsDbContext : DbContext
{
    public ApiProductsDbContext(DbContextOptions<ApiProductsDbContext> options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }
}
