using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _productService;

        public ProductController(AppDbContext productService)
        {
            _productService = productService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                // Create a new product instance
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Category = productDto.Category,
                    SellerName = productDto.SellerName,
                    PictureUrl = productDto.PictureUrl,
                    Stock = productDto.Stock
                };

                // Add the product to the DbSet and save changes to the database
                _productService.Products.Add(product);
                await _productService.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, ex.Message);
            }


        }

        [HttpGet("get")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                // Get all products from the DbSet and return as a list
                var products = await _productService.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, ex.Message);
            }
        }
    }
}