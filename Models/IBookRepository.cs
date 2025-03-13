namespace BookStore.Models
{
	public interface IBookRepository
	{
		Task<List<Book>> GetAsync();
		Task<Book?> GetByIdAsync(int id);
		Task<Book?> GetByUserIdlAsync(int id);
		Task AddAsync(Book user);
		Task UpdateAsync(Book user);
		Task DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
	}
}
