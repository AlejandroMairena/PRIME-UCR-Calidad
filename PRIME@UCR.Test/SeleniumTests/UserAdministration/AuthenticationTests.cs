using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PRIME_UCR.Test.SeleniumTests.UserAdministration
{
    public class AuthenticationTests : IDisposable
    {
        private IWebDriver driver;
        private string url = "https://localhost:44368/";

        [Fact]
        public void AuthenticationNoPasswordTest()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Url = url;

            Thread.Sleep(1000);

            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[1]/input")).SendKeys("test@test.com");
            var output = driver.FindElement(By.XPath("/html/body/app/div/div/form/div[2]/div[2]")).Text;
            Assert.Equal("Digite su contraseña", output);
        }

        [Fact]
        public void AuthenticationNoEmailTest()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Url = url;

            Thread.Sleep(1000);

            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[2]/div/input")).SendKeys("Test");
            var output = driver.FindElement(By.XPath("/html/body/app/div/div/form/div[1]/div")).Text;
            Assert.Equal("Digite su correo", output);
        }

        [Fact]
        public void AuthenticationFailedTest()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Url = "https://localhost:44368/";

            Thread.Sleep(1000);

            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[1]/input")).SendKeys("test@test.com");
            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[2]/div/input")).SendKeys("Test");
            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[5]/button")).Click();
            Thread.Sleep(3000);
            var output = driver.FindElement(By.Id("emailHelp")).Text;
            Assert.Equal("Ingresó su usuario o contraseña incorrectamente. Intente de nuevo.", output);
        }

        [Fact]
        public void AuthenticationSuccessTest()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Url = "https://localhost:44368/";

            Thread.Sleep(1000);

            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[1]/input")).SendKeys("juan.guzman@prime.com");
            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[2]/div/input")).SendKeys("Juan.Guzman10");
            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[5]/button")).Click();
            Thread.Sleep(3000);
            var output = driver.FindElement(By.XPath("/html/body/app/div/div/ul/li[1]/a")).Text;
            Assert.Equal("Dashboard", output);
        }

        private void Authenticate()
        {
            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[1]/input")).SendKeys("juan.guzman@prime.com");
            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[2]/div/input")).SendKeys("Juan.Guzman10");
            driver.FindElement(By.XPath("/html/body/app/div/div/form/div[5]/button")).Click();
        }

        [Fact]
        public void LogOutCancel()
        {
            driver = new ChromeDriver();

            driver.Url = "https://localhost:44368/";

            driver.Manage().Window.Maximize();

            Thread.Sleep(2000);

            Authenticate();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("/html/body/header/div[2]/div[2]/a")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Cancelar")).Click();
            Thread.Sleep(1000);
            var output = driver.FindElement(By.XPath("/html/body/header/div[2]/div[2]/p")).Text;
            Assert.Equal("juan.guzman@prime.com", output);
        }

        [Fact]
        public void LogOutSuccess()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Url = "https://localhost:44368/";

            Thread.Sleep(2000);

            Authenticate();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("/html/body/header/div[2]/div[2]/a")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Confirmar")).Click();
            Thread.Sleep(1000);
            var output = driver.FindElement(By.XPath("/html/body/app/main/div/b")).Text;
            Assert.Equal("Debe de registrarse antes de acceder a la página.", output);
        }

        public void Dispose()
        {
            driver.Dispose();
        }

    }
}
