namespace QaTask.Contracts.Interfaces;

public interface ILoginPage
{
    Task NavigateTo(string url);

    Task EnterUsername(string username);
    Task EnterPassword(string password);
    Task ClickLoginButton();
    Task<string?> LoginAssertion();
    Task<string?> LogoAssertion();
    Task<bool> IsLoginButtonVisibleAsync();

}