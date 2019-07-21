using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishList.Domain.Services;
using WishList.Entities;

namespace WishList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _context;

        public ProductsController(IProductServices context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet("page_size={page_size}&page={page}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int page_size = 0, int page = 0)
        {
            var lst = await _context.GetAllAsync(page_size, page);

            if (lst.Count() > 0)
            { return Ok(lst); }

            return NotFound();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (id != 0)
            {
                var product = await _context.GetSingleAsync(p => p.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            return BadRequest("Favor informar o id");
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors).Select(e => $"{e.ErrorMessage}; ").ToString());
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            //_context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors).Select(e => $"{e.ErrorMessage}; ").ToString());
            }

            await _context.InsertAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            if (id != 0)
            {
                var product = await _context.GetSingleAsync(p => p.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }

                await _context.DeleteAsync(product);

                return product;
            }
            return BadRequest("Favor informar o id");
        }

        private bool ProductExists(int id) => _context.GetSingleAsync(p => p.ProductId == id).Result != null;
    }
}