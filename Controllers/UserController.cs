using BookStore.Infrastructure;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserRepository _userRepository;

		public UserController(UserRepository userRepository)
		{
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			var users = await _userRepository.GetAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User user)
		{
			await _userRepository.AddAsync(user);
			return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutUser(int id, User user)
		{
			if (id != user.Id)
			{
				return BadRequest("ID do usuário não corresponde ao parâmetro.");
			}

			try
			{
				await _userRepository.UpdateAsync(user);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _userRepository.ExistsAsync(id))
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
			var userExists = await _userRepository.ExistsAsync(id);
			if (!userExists)
			{
				return NotFound();
			}

			await _userRepository.DeleteAsync(id);
			return NoContent();
		}
	}
}
