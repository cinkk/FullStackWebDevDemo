using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }
            return User;
        }

        // POST: api/Todo
        // when provided name and isComplete in the body of a request, adds a new item
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User item)
        {
            System.Random rnd = new System.Random();

            //Generate random id for user between 1000-9999, do this multiple times if an id matches a current user
            do
            {
                item.Id = rnd.Next(1000, 10000);
            } while (_context.Users.Find(item.Id) != null); //check if id already exists in database

            _context.Users.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        // when provided an item's ID in the body of a request and in the URL, will update an existing item
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
