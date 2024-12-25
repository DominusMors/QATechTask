using QaTask.Contracts.Models;

namespace QaTask.Contracts.Interfaces;

public interface IApiClient
{
    /// Fetch a user by username from the API.
    Task<UserModel?> GetUser(string? username);

    /// Create multiple users with a list of user data.
    Task<GenericResponse?> CreateUsersWithListAsync(List<UserModel> users);

    /// Update a user by username.
    Task<GenericResponse?> UpdateUser(string? username, UserModel updatedUser);

    /// Delete a user by username.
    Task<GenericResponse?> DeleteUser(string username);
}