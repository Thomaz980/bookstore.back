using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly Infrastructure.ConnectionContext _context;

		public BookController(Infrastructure.ConnectionContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
		{
			var books = await _context.Books.ToListAsync();
			return Ok(books);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Book>> GetBook(int id)
		{
			var book = await _context.Books.FindAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			return Ok(book);
		}

		[HttpPost]
		public async Task<ActionResult<Book>> PostBook(Book book)
		{
			if (book == null)
			{
				return BadRequest("Dados do livro inválidos.");
			}

			_context.Books.Add(book);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutBook(int id, Book book)
		{
			if (id != book.Id)
			{
				return BadRequest("ID não encontrado.");
			}

			_context.Entry(book).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				var bookExists = await _context.Books.AnyAsync(e => e.Id == id);
				if (!bookExists)
				{
					return NotFound();
				}
				throw;
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
