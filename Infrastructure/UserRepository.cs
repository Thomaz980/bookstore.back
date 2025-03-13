using BookStore.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure
{
	public class UserRepository
	{

		private readonly ConnectionContext _context;

		public UserRepository(ConnectionContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<List<User>> GetAsync()
		{
			return await _context.Users.AsNoTracking().ToListAsync();
		}

		public async Task<User?> GetByIdAsync(int id)
		{
			return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task AddAsync(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user), "O usuário não pode ser nulo.");
			}

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user), "O usuário não pode ser nulo.");
			}

			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user != null)
			{
				_context.Users.Remove(user);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _context.Users.AnyAsync(u => u.Id == id);
		}
	}
}
