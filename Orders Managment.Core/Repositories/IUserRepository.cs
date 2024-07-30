using Order_Management.Core.Entities;


namespace Order_Management.Core.Repositories
{
	public interface IUserRepository
	{
		Task<User?> GetUserByIdAsync(string userId);
		Task<User?> GetUserByUsernameAsync(string username);
		Task<IEnumerable<User?>> GetAllUsersAsync();
	}
}
