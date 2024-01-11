using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            try
            {
                var products = context.Products.Select(x => new ProductModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price
                }).ToList();

                return Ok(products);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("putProduct")]
        public IActionResult PutProduct([FromQuery] string name, string description, int groupId, int price)
        {
            try
            {
                if (!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                {
                    context.Add(new Product()
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                    });

                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return StatusCode(409);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteProduct/{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                var product = context.Products.Find(productId);

                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteGroup/{groupId}")]
        public IActionResult DeleteGroup(int groupId)
        {
            try
            {
                var group = context.Groups.Find(groupId);

                if (group != null)
                {
                    var productsInGroup = context.Products.Where(x => x.GroupId == groupId).ToList();

                    if (productsInGroup.Any())
                    {
                        context.Products.RemoveRange(productsInGroup);
                    }

                    context.Groups.Remove(group);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("updateProductPrice/{productId}")]
        public IActionResult UpdateProductPrice(int productId, [FromQuery] int newPrice)
        {
            try
            {
                var product = context.Products.Find(productId);

                if (product != null)
                {
                    product.Price = newPrice;
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
