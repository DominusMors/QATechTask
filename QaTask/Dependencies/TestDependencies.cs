using BoDi;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using QaTask.Contracts.Interfaces;
using QaTask.Dependencies.API;
using QaTask.Pages;
using Serilog;
using Serilog.Events;
using TechTalk.SpecFlow;
using ILogger = Serilog.ILogger; 

namespace QaTask.Dependencies
{
    [Binding]
    public class TestDependencies
    {
        private IPage? _page;
        private IBrowser? _browser;

        [BeforeScenario(Order = 0)]
        public void SetUpConfiguration(IObjectContainer container)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("Dependencies/settings.json", optional: false);
            container.RegisterInstanceAs<IConfiguration>(configuration.Build());
            container.RegisterTypeAs<AppConfiguration, IAppConfiguration>();
        }
        
        [BeforeScenario(Order = 2)]
        public void SetupILogger(IObjectContainer objectContainer)
        {
            var logger = new LoggerConfiguration()
                .WriteTo
                .Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
            
            objectContainer.RegisterInstanceAs<ILogger>(logger);
        }
        [BeforeScenario("@gui")]
        public async Task SetupPlaywright()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
        }

        [BeforeScenario("@gui")]
        public void RegisterInstances(IObjectContainer container, ScenarioContext scenarioContext)
        {
            container.RegisterInstanceAs(_page);
            container.RegisterInstanceAs(_browser);
            container.RegisterTypeAs<LoginPage, ILoginPage>();
            container.RegisterTypeAs<InventoryPage, IInventoryPage>();
        }

        [AfterScenario("@gui")]
        public async Task Cleanup(IObjectContainer container, ScenarioContext scenarioContext)
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
        }
        
        [BeforeScenario("@api", Order = 1)]
        public void SetUpApiClient(IObjectContainer container) 
            => container.RegisterTypeAs<ApiClient, IApiClient>();

    }
}