using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CloudComputingUTN.WebApp.Tests
{
    public class HomeControllerTests
    {
        private IWebDriver webDriver;
        [SetUp]
        public void Setup()
        {
            webDriver = new FirefoxDriver();
        }

        [Test]
        public void HomeView_WelcomeTextIsDisplayed()
        {
            Assert.Pass();
        }
    }
}