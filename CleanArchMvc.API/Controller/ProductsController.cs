using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetCategories()
        {
            var products = await _productService.GetProducts();
            if(products is null)
                return NotFound("");
            else
                return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetById(id);
            if(product is null)
                return NotFound("Product not found");
            else
                return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductDTO productDTO)
        {
            if(productDTO is null)
                return BadRequest();
            
            await _productService.Add(productDTO);

            return new CreatedAtRouteResult("GetProduct",
            new {id = productDTO.Id}, productDTO);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }
            if (productDTO is null)
            {
                return BadRequest();
            }

            // var product = await _productService.GetById(id);
            // if(product is null)
            //     return NotFound();
            
            await _productService.Update(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
        {
            var product = await _productService.GetById(id);
            if(product is null)
                return NotFound("Product NotFoundNotFound");
            
            await _productService.Remove(id);
            return Ok(product);
        }
    }
}
