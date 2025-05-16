using Microsoft.AspNetCore.Mvc;
namespace APIChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    public static Product[] products = {
        new Product { ProductId = 1, ProductName = "Product1", Capacity = 0, Quantity = 0},
        new Product { ProductId = 2, ProductName = "Product2", Capacity = 0, Quantity = 0},
        new Product { ProductId = 3, ProductName = "Product3", Capacity = 0, Quantity = 0},
        new Product { ProductId = 4, ProductName = "Product4", Capacity = 0, Quantity = 0},
        new Product { ProductId = 5, ProductName = "Product5", Capacity = 0, Quantity = 0},
        new Product { ProductId = 6, ProductName = "Product6", Capacity = 0, Quantity = 0},
        new Product { ProductId = 7, ProductName = "Product7", Capacity = 0, Quantity = 0},
        new Product { ProductId = 8, ProductName = "Product8", Capacity = 0, Quantity = 0},
        new Product { ProductId = 9, ProductName = "Product9", Capacity = 0, Quantity = 0},
        new Product { ProductId = 10, ProductName = "Product10", Capacity = 0, Quantity = 0},
    };

    // [HttpGet]
    // public Task<IEnumerable<Product>> GetProducts()
    // {
    //     return products.AsEnumerable();
    // }

    [HttpPut("/{productId}")]
    public IActionResult SetProductCapacity(int productId, int capacity)
    {
        var found = products.FirstOrDefault(p => p.ProductId == productId);

        // return Ok(found);

        if (found is null)
        {
            return NoContent();
        }

        if (capacity <= 0)
        {
            return BadRequest("Capacity is required or different to 0.");
        }

        found.Capacity = capacity;

        return Ok("Capacity updated.");
    }

    [HttpPut("/receive/{productId}")]
    public IActionResult ReceiveProduct(int productId, int qty)
    {
        var found = products.FirstOrDefault(p => p.ProductId == productId);

        if (found is null)
        {
            return NoContent();
        }

        if (found.Capacity == 0)
        {
            return BadRequest("First set capacity to the product.");
        }

        if (qty > found.Capacity || qty <= 0)
        {
            return BadRequest("The quantity cant be asigned.");
        }

        if ((qty + found.Quantity) > found.Capacity)
        {
            return BadRequest("This value cant be assgined to the correct Capacity.");
        }

        found.Quantity = +qty;

        return Ok("Quantity updated.");
    }

    [HttpPut("/dispatch/{productId}")]
    public IActionResult DispatchProduct(int productId, int qty)
    {
        var found = products.FirstOrDefault(p => p.ProductId == productId);

        if (found is null)
        {
            return NoContent();
        }

        if (found.Capacity == 0)
        {
            return BadRequest("First set capacity to the product.");
        }

        if (qty <= 0)
        {
            return BadRequest("This quantity cant be asigned.");
        }

        if ((found.Capacity - qty) < 0)
        {
            return BadRequest("This value cant be assgined to the correct Capacity.");
        }

        if ((found.Quantity - qty) < 0)
        {
            return BadRequest("This value cant be assgined to the correct Capacity.");
        }

        found.Quantity = found.Quantity - qty;

        return Ok("Quantity updated.");
    }
}
