namespace QaTask.Contracts.Interfaces;

public interface IAppConfiguration
{
    string BaseUrl { get; }
    string RestApiUrl { get; }
    string ValidUsername { get; }
    string ValidPassword { get; }
    string InvalidUsername { get; }
    string InvalidPassword { get; }
    string LockedUserMessage { get; }
    string LogoText { get; }
    string FirstName { get; }
    string LastName { get; }
    string PostalCode { get; }
    string FirstNameError { get; }
    string LastNameError { get; }
    string PostalCodeError { get; }
    string BackPackPrice { get; }
    string FleeceJacketPrice { get; }
    string TshirtPrice { get; }
}