using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Repositories;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _unitOfWork.Products.CreateAsync(product);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
