using FluentAssertions;
using Newtonsoft.Json;
using QaTask.Contracts.Models;
using QaTask.Dependencies.API;
using TechTalk.SpecFlow;

namespace QaTask.StepDefinitions.API
{
    [Binding]
    public class UserOperationsStepDefinitions(ApiClient apiClient, ScenarioContext scenarioContext)
    {
        private const string ContextUserKey = "CurrentUser";

        [When(@"I request the user ""(.*)"" from the API")]
        public async Task RequestUserFromApi(string? username)
        {
            var user = await apiClient.GetUser(username);
            
            if (user != null)
            {
                scenarioContext.Set(user, ContextUserKey);    
            }
        } 

        [Then(@"the response should contain user data for ""(.*)""")]
        public void GetUserDataFromUsername(string username)
        {
            var user = scenarioContext.Get<UserModel?>(ContextUserKey);
            
            user.Should().NotBeNull(because: "the response should not be null");
            user!.Username.Should().BeEquivalentTo(username, because: $"the response should contain user data for {username}");
        }

        [Given("I have a user with the following data")]
        [When("I create a user with the following data")]
        public async Task CreateANewUser(Table table)
        {
            var row = table.Rows[0];
            
            var user = CreateUserModel(
                Convert.ToInt64(row["id"]),
                row["username"],
                row["firstName"],
                row["lastName"],
                row["email"],
                row["password"],
                row["phone"],
                Convert.ToInt32(row["userStatus"]));
            
            var response = await apiClient.CreateUsersWithListAsync([user]);
            AssertSuccessfulResponse(response);
            
            scenarioContext.Set(user, ContextUserKey);
        }

        [Then(@"assert that the user ""(.*)"" has been created")]
        public void VerifyUserCreation(string username)
        {
            var createdUser = scenarioContext.Get<UserModel>(ContextUserKey);
            
            createdUser.Should().NotBeNull($"User {username} should have been created successfully");
            createdUser!.Username.Should().Be(username, $"the user {username} should have been created successfully");
        }
        
        [When(@"I update the user ""(.*)"" with the following data")]
        public async Task UpdateUserData(string? username, Table table)
        {
            var row = table.Rows[0];

            var user = scenarioContext.Get<UserModel>(ContextUserKey);
            user.Should().NotBeNull();

            var updatedUser = CreateUserModel(
                Convert.ToInt64(row["id"]),
                row["username"],
                row["firstName"],
                row["lastName"],
                row["email"],
                row["password"],
                row["phone"],
                Convert.ToInt32(row["userStatus"]));
            
            var updateUserResponse = await apiClient.UpdateUser(username, updatedUser);
            AssertSuccessfulResponse(updateUserResponse);

            scenarioContext.Set(updatedUser, ContextUserKey);
        }

        [Then(@"the response should confirm the user details are updated as follows")]
        public void ConfirmUserDetailsAreUpdated(Table table)
        {
            var user = scenarioContext.Get<UserModel>(ContextUserKey);
            user.Should().NotBeNull();

            var row = table.Rows[0];

            var expectedUser = CreateUserModel(
                Convert.ToInt64(row["id"]),
                row["username"],
                row["firstName"],
                row["lastName"],
                row["email"],
                row["password"],
                row["phone"],
                Convert.ToInt32(row["userStatus"]));

            user!.Username.Should().BeEquivalentTo(expectedUser.Username);
            user!.FirstName.Should().BeEquivalentTo(expectedUser.FirstName);
            user!.LastName.Should().BeEquivalentTo(expectedUser.LastName);
            user!.Email.Should().BeEquivalentTo(expectedUser.Email);
            user!.Password.Should().BeEquivalentTo(expectedUser.Password);
            user!.Phone.Should().BeEquivalentTo(expectedUser.Phone);
            user!.UserStatus.Should().Be(expectedUser.UserStatus);
        }

        [When(@"I delete the user ""(.*)"" from the API")]
        public async Task DeleteUser(string username)
        {
            var user = scenarioContext.Get<UserModel>(ContextUserKey);
            user.Should().NotBeNull();
            
            var response = await apiClient.DeleteUser(username);
            AssertSuccessfulResponse(response);
            
            scenarioContext.Remove(ContextUserKey);
        }

        [Then(@"the response should confirm the user ""(.*)"" is deleted")]
        public void ConfirmUserisDeleted(string username)
        {
            scenarioContext
                .Invoking(ctx => ctx.Get<UserModel>(ContextUserKey))
                .Should().Throw<KeyNotFoundException>();
        }

        private void AssertSuccessfulResponse(GenericResponse? response)
        {
            response.Should().NotBeNull();
            response!.Code.Should().Be(200);
        }

        private UserModel CreateUserModel(long id, string username, string firstName, string lastName, string email,
            string password, string phone, int userStatus) =>
            new()
            {
                Id = id,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Phone = phone,
                UserStatus = userStatus
            };
    }
}