using System.Collections.Frozen;
using Microsoft.Playwright;
using QaTask.Contracts.Enums;
using QaTask.Contracts.Interfaces;

namespace QaTask.Pages;

public class LoginPage(IPage page) : LocatablePageBase<LoginPageLocators>(page), ILoginPage
{
    protected override FrozenDictionary<LoginPageLocators, string> LocatorTemplates { get; set; } =
        new Dictionary<LoginPageLocators, string>
        {
            [LoginPageLocators.UsernameField] = "//input[@id='user-name']",
            [LoginPageLocators.PasswordField] = "//input[@id='password']",
            [LoginPageLocators.LoginButton] = "//input[@id='login-button']",
            [LoginPageLocators.ErrorMessageField] = "//h3[@data-test='error']",
            [LoginPageLocators.AppLogoField] = "//div[@class='app_logo']"
        }.ToFrozenDictionary();

    public Task EnterUsername(string username) => GetLocator(LoginPageLocators.UsernameField).FillAsync(username);
    public Task EnterPassword(string password) => GetLocator(LoginPageLocators.PasswordField).FillAsync(password);
    public Task ClickLoginButton() => GetLocator(LoginPageLocators.LoginButton).ClickAsync();
    public Task<string?> LoginAssertion() => GetLocator(LoginPageLocators.ErrorMessageField).TextContentAsync();
    public Task<string?> LogoAssertion() => GetLocator(LoginPageLocators.AppLogoField).TextContentAsync();

    public async Task<bool> IsLoginButtonVisibleAsync()
    {
        try
        {
            await GetLocator(LoginPageLocators.UsernameField).WaitForAsync(new LocatorWaitForOptions
                { State = WaitForSelectorState.Visible });
            return true;
        }
        catch (TimeoutException)
        {
            return false;
        }
    }
}