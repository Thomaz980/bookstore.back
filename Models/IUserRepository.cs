﻿namespace BookStore.Models
{
	public interface IUserRepository
	{
		Task<List<User>> GetAsync();
		Task<User?> GetByIdAsync(int id);
		Task<User?> GetByEmailAsync(string email);
		Task AddAsync(User user);
		Task UpdateAsync(User user);
		Task DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
		Task<bool> ExistsByEmailAsync(string email);
	}
}
