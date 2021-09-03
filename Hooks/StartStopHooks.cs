using BoDi;
using NLA.Tests.UI.PageObjects;
using NLA.Tests.UI.Utils;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Xunit;

namespace NLA.Tests.UI.Hooks
{
    [Binding]
    class StartStopHooks
    {
        private static IConfiguration _configs;
        private readonly IObjectContainer _objectContainer;

        public StartStopHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void LoadConfigurations()
        {
            _configs = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
        }

        [BeforeScenario(Order = 1)]
        public void SetConfigurations()
        {
            _objectContainer.RegisterInstanceAs(_configs);
        }

        [BeforeScenario(Order = 3)]
        public void CreateWebDriver(ScenarioContext context)
        {
            DriverFacade driverFacade = new DriverFacade(_configs);
            _objectContainer.RegisterInstanceAs(driverFacade);
        }

        [BeforeScenario(Order = 4)]
        [Scope(Tag = "login")]
        public void LoginToSystem(ScenarioContext context, LoginPage loginPage, DashboardPage dashboardPage)
        {
            loginPage.Navigate();
            loginPage.UserName.Text = _configs["SystemUser:Name"];
            loginPage.Password.Text = _configs["SystemUser:Password"];
            loginPage.Account.Text = _configs["SystemUser:Tenant"];
            loginPage.SignIn.Click();
            Assert.True(dashboardPage.GlobalSearch.IsFound, "Not logged on to the system. Cannot proceed.");
        }

        [AfterScenario(Order = 1)]
        public void CloseWebDriver(ScenarioContext context, DriverFacade driverFacade)
        {
            driverFacade.Quit();
        }
    }
}
