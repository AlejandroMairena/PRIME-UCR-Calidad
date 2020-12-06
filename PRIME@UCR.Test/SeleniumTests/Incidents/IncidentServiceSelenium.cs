using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace PRIME_UCR.Test.SeleniumTests.Incidents
{
 
    public class IncidentServiceSelenium : IDisposable
    {
        IWebDriver driver;
        private string IncidentsUrl = "https://localhost:44368/incidents";
        public void Dispose()
        {
            driver.Dispose();
        }
        /*
        [Fact]
        public void NotAuthMessageChrome()
        {
            driver = new ChromeDriver();
            NotAuthMessage();
        }
        */
        [Fact]
        public void NotAuthMessageFirefox()
        {
            driver = new FirefoxDriver();
            NotAuthMessage();
        }

        private void NotAuthMessage()
        {
            //Wait time, TODO: search if its needed
            _ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            ///Act
            driver.Url = IncidentsUrl;
            ///Assert
            Assert.Equal("Para acceder a esta página debe registarse.", driver.FindElement(By.XPath("//div[@class='content px-4 float-none']//p[1]")).Text);
        }
    }
}
