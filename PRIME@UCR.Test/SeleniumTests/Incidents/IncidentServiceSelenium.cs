using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Xunit;

namespace PRIME_UCR.Test.SeleniumTests.Incidents
{
 
    public class IncidentServiceSelenium : IDisposable
    {
        IWebDriver driver;
        IWebElement Element;
        SelectElement oSelect;
        private string IncidentsUrl = "https://localhost:44368/incidents";

        private int TimeoutMiliseconds = 5000;

        public void Dispose()
        {
            driver.Dispose();
        }

        [Fact]
        public void GetToIncidentListPage()
        {
            driver = new ChromeDriver();
            AccessToIncidentPage();
            Element = TryToFind("//h1");
            //Element = driver.FindElement(By.XPath("//h1"));//Find input for password
            Assert.Equal("Lista de Incidentes", Element.Text);
        }

        [Fact]
        public void ViewIncidentDetails()
        {
            driver = new ChromeDriver();
            AccessToIncidentPage();
            AccessToFirstIncident();
            //Timeout(1000);
            //Element = driver.FindElement(By.XPath("//h1"));
            Element = TryToFind("//h1");
            Assert.Equal("Administración del incidente", Element.Text);
        }

        [Fact]
        public void FillIncidentInfo()
        {
            driver = new ChromeDriver();
            AccessToIncidentPage();
            AccessToFirstIncident();
            MockInfoInIncident();
        }

        private void MockInfoInIncident()
        {
            MockOriginTab();
        }

        private void MockOriginTab()
        {
            Element = TryToFind("//div[@class='row']/div/ul/li[2]");//Origin tab
            Element.Click();
            Element = TryToFindById("Tipo");//Dropdown menu tipo de origen
            oSelect = new SelectElement(Element);
            oSelect.SelectByIndex(1);//Select Internacional en el dropdown menu
            Element = TryToFindById("Pais");
            oSelect = new SelectElement(Element);
            oSelect.SelectByIndex(1);//Select El salvador en el dropdown menu
            Element = TryToFindById("Guardar");//Find guardar button
            Element.Click();
            Timeout(3000);
        }

        private void MockDestinationTab()
        {
            Element = TryToFind("//div[@class='row']/div/ul/li[3]");//Destination tab
            Element.Click();

        }

        private IWebElement TryToFind(string path)
        {
            IWebElement _element = null;
            bool exit = false;
            //OpenQA.Selenium.NoSuchElementException
            while (!exit)
            {
                exit = true;
                try
                {
                    _element = driver.FindElement(By.XPath(path));
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    exit = false;
                }
            }
            return _element;
        }

        private IWebElement TryToFindById(string Id)
        {
            IWebElement _element = null;
            bool exit = false;
            //OpenQA.Selenium.NoSuchElementException
            while (!exit)
            {
                exit = true;
                try
                {
                    _element = driver.FindElement(By.Id(Id));
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    exit = false;
                }
            }
            return _element;
        }

        private void AccessToFirstIncident()
        {
            Element = TryToFind("//td/a");//Find incident code to click
            //Element = driver.FindElement(By.XPath("//td/a"));//Find incident code to click
            Element.Click();
            Timeout(1000);
        }

        private void Timeout(int miliseconds)
        {
            Thread.Sleep(miliseconds);
        }

        private void AccessToIncidentPage()
        {
            //Wait time, TODO: search if its needed
            //_ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = IncidentsUrl;//Go to page
            driver.Manage().Window.Maximize();
            //Timeout(TimeoutMiliseconds);
            Element = driver.FindElement(By.Id("Correo"));//Find input for mail
            Element.Clear();
            Element.SendKeys("teodoro.barquero@prime.com");//Mock
            //Timeout(TimeoutMiliseconds);
            Element = driver.FindElement(By.Id("Contrasena"));//Find input for password
            Element.Clear();
            Element.SendKeys("Teodoro.Barquero10");//Mock
            driver.FindElement(By.Id("Ingresar")).Click();//Ingresar página
            //Timeout(TimeoutMiliseconds);//Wait for validation
        }
    }
}
