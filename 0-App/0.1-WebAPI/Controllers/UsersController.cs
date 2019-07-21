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
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _context;

        public UsersController(IUserServices context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet("page_size={page_size}&page={page}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int page_size = 0, int page = 0)
        {
            var lst = await _context.GetAllAsync(page_size, page);

            if (lst.Count() > 0)
            { return Ok(lst); }

            return NotFound();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (id != 0)
            {
                var user = await _context.GetSingleAsync(u => u.UserId == id);

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            return BadRequest("Favor informar o id");
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors).Select(e => $"{e.ErrorMessage}; ").ToString());
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            //_context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _context.InsertAsync(user);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            if (id != 0)
            {
                var user = await _context.GetSingleAsync(u => u.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }

                await _context.DeleteAsync(user);

                return user;
            }
            return BadRequest("Favor informar o id");
        }

        private bool UserExists(int id) => _context.GetSingleAsync(u => u.UserId == id).Result != null;
    }
}