using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetUserByLoginIdentifier(string loginName);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IEnumerable<User>> GetUsersByDepartmentAsync(string department);
    Task<User> UpdateUserAsync(string id, User updatedUser);
    Task<bool> DeleteUserAsync(string id);
}
