using Microsoft.EntityFrameworkCore;
using Order_Management.Repository.Data;
using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;

namespace Order_Management.Repository.Repo_Implementation
{
	public class UserRepository : IUserRepository
	{
		private readonly OrderManagementDbContext _context;

		public UserRepository(OrderManagementDbContext context)
		{
			_context = context;
		}

		public async Task<User?> GetUserByIdAsync(string userId)
		{
			return await _context.Users.FindAsync(userId);
		}

		public async Task<User?> GetUserByUsernameAsync(string username)
		{
			return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
		}

		public async Task<IEnumerable<User?>> GetAllUsersAsync()
		{
			return await _context.Users.ToListAsync();
		}
	}
}
