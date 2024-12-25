using FluentAssertions;
using QaTask.Contracts.Interfaces;
using TechTalk.SpecFlow;

namespace QaTask.StepDefinitions.GUI
{
    [Binding]
    public class LoginStepDefinitions(
        IAppConfiguration appConfiguration,
        ILoginPage loginPage,
        IInventoryPage inventoryPage)
    {
        //Step Definitions
        [StepDefinition(@"I navigate to the login page")]
        public async Task NavigateToLoginPage()
            => await loginPage.NavigateTo(appConfiguration.BaseUrl);

        [StepDefinition(@"I try to login with valid credentials")]
        public async Task LoginWithValidCredentials()
        {
            await loginPage.EnterUsername(appConfiguration.ValidUsername);
            await loginPage.EnterPassword(appConfiguration.ValidPassword);
            await loginPage.ClickLoginButton();
        }

        [When(@"I try to login with credentials of a locked user")]
        public async Task LoginWithInvalidCredentials()
        {
            await loginPage.EnterUsername(appConfiguration.InvalidUsername);
            await loginPage.EnterPassword(appConfiguration.InvalidPassword);
            await loginPage.ClickLoginButton();
        }

        [Then(@"I can assert that the correct error message appears")]
        public async Task AssertInvalidLoginMessage()
        {
            var errorMessage = await loginPage.LoginAssertion();
            errorMessage.Should().BeEquivalentTo(appConfiguration.LockedUserMessage);
        }

        [Then(@"I can assert that i have been logged in")]
        public async Task AssertValidLoginMessage()
        {
            var logoMessage = await loginPage.LogoAssertion();
            logoMessage.Should().BeEquivalentTo(appConfiguration.LogoText);
        }

        [When(@"I try to log out from the site")]
        public void LogoutFromSite()
        {
            inventoryPage.OpenBurgerMenu();
            inventoryPage.ClickLogoutButton();
        }

        [Then(@"I can assert that i have been logged out")]
        public async Task AssertLoggedOut()
        {
            var isVisible = await loginPage.IsLoginButtonVisibleAsync();
            isVisible.Should().BeTrue();
        }
    }
}