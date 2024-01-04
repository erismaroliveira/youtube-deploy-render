using ApiProducts.Context;
using ApiProducts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProducts.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ApiProductsDbContext _context;

    public ProductsController(ApiProductsDbContext context)
    {
      _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      return await _context.Products.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
      var product = await _context.Products.FindAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
    {
      product.Id = Guid.NewGuid();
      await _context.Products.AddAsync(product);
      await _context.SaveChangesAsync();
      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<Product>> PutProduct(Guid id, [FromBody] Product product)
    {
      var prod = await _context.Products.FindAsync(id);

      if (id == Guid.Empty || prod == null)
      {
        return BadRequest();
      }

      prod.Name = product.Name;
      prod.Price = product.Price;

      _context.Entry(prod).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return Ok(prod);
    }

    [HttpDelete]
    public async Task<ActionResult<Product>> DeleteProduct(Guid id)
    {
      var prod = await _context.Products.FindAsync(id);

      if (prod == null)
      {
        return NotFound();
      }

      _context.Products.Remove(prod);
      await _context.SaveChangesAsync();

      return Ok(prod);
    }
}