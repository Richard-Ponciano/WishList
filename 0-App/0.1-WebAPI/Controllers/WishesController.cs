using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Domain.Services;
using WishList.Entities;

namespace WishList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly IWishServices _context;

        public WishesController(IWishServices context)
        {
            _context = context;
        }

        //GET: api/Wishes
        [HttpGet("GetWish")]
        public async Task<ActionResult<WishLst>> GetWish(int userId, int productId)
        {
            return await _context.GetSingleAsync(w => w.UserId == userId && w.ProductId == productId);
        }

        // GET: api/Wishes/5
        [HttpGet("GetByUser/{userId}/page_size={page_size}&page={page}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetWishLst(int userId, int page_size = 0, int page = 0)
        {
            var wishLst = await _context.GetWishesByUserId(userId, page_size, page);

            if (wishLst == null)
            {
                return NotFound();
            }

            return Ok(wishLst);
        }

        // PUT: api/Wishes/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWishLst(int id, WishLst wishLst)
        //{
        //    if (id != wishLst.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    //_context.Entry(wishLst).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.InsertAsync(wishLst);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WishLstExists(wishLst.UserId, wishLst.ProductId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Wishes
        [HttpPost]
        public async Task<ActionResult<WishLst>> PostWishLst(WishLst wishLst)
        {
            try
            {
                if (!WishLstExists(wishLst.UserId, wishLst.ProductId))
                {
                    var entity = await _context.InsertAsync(wishLst);

                    return StatusCode(201, entity);
                }
                else
                { return Conflict(); }
            }
            catch (DbUpdateException)
            {
                if (WishLstExists(wishLst.UserId, wishLst.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Wishes/5
        [HttpDelete("{userId}/{productdId}")]
        public async Task<ActionResult<WishLst>> DeleteWishLst(int userId, int productdId)
        {
            var wishLst = await _context.GetSingleAsync(w => w.UserId == userId && w.ProductId == productdId);
            if (wishLst == null)
            {
                return NotFound();
            }

            await _context.DeleteAsync(wishLst);

            return wishLst;
        }

        private bool WishLstExists(int userId, int productdId) =>
            _context.GetSingleAsync(w => w.UserId == userId && w.ProductId == productdId).Result != null;
    }
}