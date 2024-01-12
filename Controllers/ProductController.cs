using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.Abstraction;
using WebApi.Models;
using WebApi.Models.DTO;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;



        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("get_groups")]
        public IActionResult GetGroups()
        {
            var groups = _productRepository.GetGroups();
            return Ok(groups);
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProbuctDto productDto)
        {
            var result = _productRepository.AddProduct(productDto);
            return Ok(result);
        }

        [HttpPost("add_group")]
        public IActionResult AddGroup([FromBody] GroupDto groupDto)
        {
            var result = _productRepository.AddGroup(groupDto);
            return Ok(result);
        }
        /*
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
        */
        /*
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
            
        */
    }
}