using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
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
        //DZ-2
        [HttpGet("export_products_csv")]
        public IActionResult ExportProductsCsv()
        {
            var products = _productRepository.GetProducts();

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(products);
                writer.Flush();
                memoryStream.Position = 0;

                return File(memoryStream, "text/csv", "products.csv");
            }
        }
        //DZ-2
        [HttpGet("cache_statistics_csv")]
        public IActionResult GetCacheStatisticsCsv()
        {
            var cacheStatistics = _productRepository.GetCacheStatistics();

            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecord(cacheStatistics);
                writer.Flush();
                memoryStream.Position = 0;

                return File(memoryStream, "text/csv", "cache_statistics.csv");
            }




        }
    }
}