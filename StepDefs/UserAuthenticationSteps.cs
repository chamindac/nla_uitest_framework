using NLA.Tests.UI.PageObjects;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using Xunit;

namespace NLA.Tests.UI.StepDefs
{
    [Binding]
    public class UserAuthenticationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IConfiguration _configs;
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;

        public UserAuthenticationSteps(ScenarioContext scenarioContext, IConfiguration configs, 
            LoginPage loginPage, DashboardPage dashboardPage)
        {
            _scenarioContext = scenarioContext;
            _configs = configs;
            _loginPage = loginPage;
            _dashboardPage = dashboardPage;
        }

        [Given(@"that I am in the login page")]
        public void GivenThatIAmInTheLoginPage()
        {
            _loginPage.Navigate();
        }
        
        [When(@"the correct username, password and account provided")]
        public void WhenTheCorrectUsernamePasswordAndAccountProvided()
        {
            _loginPage.UserName.Text = _configs["SystemUser:Name"];
            _loginPage.Password.Text = _configs["SystemUser:Password"];
            _loginPage.Account.Text = _configs["SystemUser:Tenant"];
        }

        [When(@"logged in")]
        public void WhenLoggedIn()
        {
            _loginPage.SignIn.Click();
        }


        [Then(@"the system dashboard should be shown")]
        public void ThenTheSystemDashboardShouldBeShown()
        {
            Assert.True(_dashboardPage.GlobalSearch.IsFound, "Login with correct username pwd and account failed.");
        }
    }
}
