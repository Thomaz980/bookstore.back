using BookStore.Models;
using Microsoft.AspNetCore.Connections;

namespace BookStore.Infrastructure
{
	public class BookRepository
	{
		private readonly ConnectionContext _context;

		public BookRepository(ConnectionContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<List<Book>> GetAsync()
		{
			return await _context.Books.AsNoTracking().ToListAsync();
		}

		public async Task<Book?> GetByIdAsync(int id)
		{
			return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task<Book?> GetByUserIdlAsync(int userId)
		{
			return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.UserId == userId);
		}

		public async Task AddAsync(Book book)
		{
			if (book == null)
			{
				throw new ArgumentNullException(nameof(book), "O livro não pode ser nulo.");
			}

			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Book book)
		{
			if (book == null)
			{
				throw new ArgumentNullException(nameof(book), "O livro não pode ser nulo.");
			}

			_context.Books.Update(book);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book != null)
			{
				_context.Books.Remove(book);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _context.Books.AnyAsync(b => b.Id == id);
		}
	}
}
