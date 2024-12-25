using Newtonsoft.Json;
using RestSharp;
using QaTask.Contracts.Models;
using QaTask.Contracts.Interfaces;
using Serilog;

namespace QaTask.Dependencies.API
{
    public class ApiClient(ILogger logger, IAppConfiguration configuration) : IApiClient
    {
        private readonly RestClient _client = new(configuration.RestApiUrl);

        /// Fetch a user by username from the API.
        public async Task<UserModel?> GetUser(string? username)
        {
            var request = new RestRequest($"user/{username}", Method.Get);
            try
            {
                return await SendRequest<UserModel>(request);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unable to get user with username '{Username}", username);
                return null;
            }
        }

        /// Create multiple users with a list of user data.
        public async Task<GenericResponse?> CreateUsersWithListAsync(List<UserModel> users)
        {
            var request = new RestRequest("user/createWithList", Method.Post)
                .AddJsonBody(users); // Add the serialized list of users to the body of the request
            try
            {
                return await SendRequest<GenericResponse?>(request);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unable to create users from list: {Users}", string.Join(", ", users.Select(x => x.Username)));
                return null;
            }
        }

        /// Update a user by username.
        public async Task<GenericResponse?> UpdateUser(string? username, UserModel updatedUser)
        {
            var request = new RestRequest($"user/{username}", Method.Put)
                .AddJsonBody(updatedUser); // Add the updated user data to the request body

            try
            {
                return await SendRequest<GenericResponse>(request);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unable to update user with username '{Username}'", username);
                return null;
            }
        }

        /// Delete a user by username.
        public async Task<GenericResponse?> DeleteUser(string username)
        {
            var request = new RestRequest($"user/{username}", Method.Delete);
            
            try
            {
                return await SendRequest<GenericResponse>(request);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unable to update user with username '{Username}'", username);
                return null;
            }
        }

        private async Task<T?> SendRequest<T>(RestRequest request)
        {
            var response = await _client.ExecuteAsync(request);
            
            return response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content)
                ? JsonConvert.DeserializeObject<T?>(response.Content)
                : throw new ApplicationException($"Error: Received malformed response. Status code = {response.StatusCode}");
        }
    }
}