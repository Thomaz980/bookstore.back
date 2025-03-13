using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly DbContext _context;

		public UsersController(DbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			return await _context.Set<User>().Include(u => u.Book).ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var user = await _context.Set<User>().Include(u => u.Book).FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User user)
		{
			_context.Set<User>().Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutUser(int id, User user)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}

			_context.Entry(user).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Set<User>().Any(e => e.Id == id))
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

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _context.Set<User>().FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			_context.Set<User>().Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
