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
    public class ProfileSelectionTests 
    {
        private IWebDriver driver;
        private string url = "https://localhost:44368/";

        [Fact]
        public void AssignNewPermission()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Url = url;

            Thread.Sleep(1000);
            Login();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("/html/body/app/div/div/aside/nav/div/ul/li[7]/a")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[1]/ul/li[2]/a")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/div[1]/div[1]/div/div/input")).SendKeys("129454321");
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/div[1]/div[2]/div/div/input")).SendKeys("Luis");
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/div[1]/div[3]/div[1]/div/input")).SendKeys("Morales");
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/div[1]/div[4]/div/div/input")).SendKeys("27/12/2000");
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/div[2]/div[1]/div/div/input")).SendKeys("luis.morales@prime.com");
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/div[2]/div[2]/div[1]/div/input")).SendKeys("84312743");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/div[3]/div/table/tbody/tr[1]/td[1]/center/div")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/form/button")).Click();
            Thread.Sleep(5000);

            var expected = "El usuario indicado se ha registrado en la aplicación y se le ha enviado un correo de validación de cuenta.";
            var output = driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/div")).Text;
            Thread.Sleep(1000);

            Assert.Equal(expected,output);
        }


        [Fact]
        public void resendConfirmationEmail()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Url = url;

            Thread.Sleep(1000);
            Login();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("/html/body/app/div/div/aside/nav/div/ul/li[7]/a")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[1]/ul/li[3]/a")).Click();
            Thread.Sleep(1000);


            driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/div/table/tbody/tr[4]/td[1]/center/button")).Click();
            Thread.Sleep(5000);

            var expected = "Se ha reenviado un correo de validación de cuenta al usuario indicado.";
            var output = driver.FindElement(By.XPath("/html/body/app/div/div/main/div/div/div[2]/div[1]")).Text;
            Thread.Sleep(1000);

            Assert.Equal(expected, output);
        }

        private void Login()
        {
            driver.FindElement(By.XPath("/html/body/app/div/div/aside/nav/div/form/div[1]/input")).SendKeys("juan.guzman@prime.com");
            driver.FindElement(By.XPath("/html/body/app/div/div/aside/nav/div/form/div[2]/div/input")).SendKeys("Juan.Guzman10");
            driver.FindElement(By.XPath("/html/body/app/div/div/aside/nav/div/form/div[5]/button")).Click();
        }


    }
}
