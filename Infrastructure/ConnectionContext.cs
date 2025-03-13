using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.Infrastructure
{
	public class ConnectionContext : DbContext
	{
		public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Book> Books { get; set; }
	}
}