using System.Configuration;
using Microsoft.Extensions.Configuration;
using QaTask.Contracts.Interfaces;

namespace QaTask.Dependencies
{
    public class AppConfiguration(IConfiguration configuration) : IAppConfiguration
    {
        public string BaseUrl => configuration["TestSettings:BaseUrl"]
                                 ?? throw new ConfigurationErrorsException(
                                     "Missing configuration: TestSettings:BaseUrl");

        public string RestApiUrl => configuration["TestSettings:RestApiUrl"]
                                    ?? throw new ConfigurationErrorsException(
                                        "Missing configuration: TestSettings:RestApiUrl");

        public string ValidUsername => configuration["ValidUserCredentials:username"]
                                       ?? throw new ConfigurationErrorsException(
                                           "Missing configuration: ValidUserCredentials:username");

        public string ValidPassword => configuration["ValidUserCredentials:password"]
                                       ?? throw new ConfigurationErrorsException(
                                           "Missing configuration: ValidUserCredentials:password");

        public string InvalidUsername => configuration["InvalidUserCredentials:username"]
                                         ?? throw new ConfigurationErrorsException(
                                             "Missing configuration: InvalidUserCredentials:username");

        public string InvalidPassword => configuration["InvalidUserCredentials:password"]
                                         ?? throw new ConfigurationErrorsException(
                                             "Missing configuration: InvalidUserCredentials:password");

        public string LockedUserMessage => configuration["InvalidErrorMessage:lockedusermessage"]
                                           ?? throw new ConfigurationErrorsException(
                                               "Missing configuration: InvalidErrorMessage:lockedusermessage");

        public string LogoText => configuration["LogoAssertion:logotext"]
                                  ?? throw new ConfigurationErrorsException(
                                      "Missing configuration: LogoAssertion:logotext");

        public string FirstName => configuration["ValidPersonalInformation:firstname"]
                                   ?? throw new ConfigurationErrorsException(
                                       "Missing configuration: ValidPersonalInformation:firstname");

        public string LastName => configuration["ValidPersonalInformation:lastname"]
                                  ?? throw new ConfigurationErrorsException(
                                      "Missing configuration: ValidPersonalInformation:lastname");

        public string PostalCode => configuration["ValidPersonalInformation:postalcode"]
                                    ?? throw new ConfigurationErrorsException(
                                        "Missing configuration: ValidPersonalInformation:postalcode");

        public string FirstNameError => configuration["PersonalInformationErrorMessages:firstnameError"]
                                        ?? throw new ConfigurationErrorsException(
                                            "Missing configuration: PersonalInformationErrorMessages:firstnameError");

        public string LastNameError => configuration["PersonalInformationErrorMessages:lastnameError"]
                                       ?? throw new ConfigurationErrorsException(
                                           "Missing configuration: PersonalInformationErrorMessages:lastnameError");

        public string PostalCodeError => configuration["PersonalInformationErrorMessages:postalcodeError"]
                                         ?? throw new ConfigurationErrorsException(
                                             "Missing configuration: PersonalInformationErrorMessages:postalcodeError");
        public string BackPackPrice => configuration["ItemsNames:Backpack"]
                                         ?? throw new ConfigurationErrorsException(
                                             "Missing configuration: PersonalInformationErrorMessages:postalcodeError");
        public string FleeceJacketPrice => configuration["ItemsNames:FleeceJacket"]
                                         ?? throw new ConfigurationErrorsException(
                                             "Missing configuration: PersonalInformationErrorMessages:postalcodeError");
        public string TshirtPrice => configuration["ItemsNames:Tshirt"]
                                         ?? throw new ConfigurationErrorsException(
                                             "Missing configuration: PersonalInformationErrorMessages:postalcodeError");
    }
}