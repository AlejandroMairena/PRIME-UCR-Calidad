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
        public void FillInfoOriginTab()
        {
            driver = new ChromeDriver();
            AccessToIncidentPage();
            AccessToFirstIncident();
            MockOriginTab();
            string text = CheckSaveState("Mensaje de estado", "Se guardaron los cambios exitosamente.");
            Assert.Equal("Se guardaron los cambios exitosamente.", text);
        } 

        [Fact]
        public void FillInfoDestinationTab()
        {
            driver = new ChromeDriver();
            AccessToIncidentPage();
            AccessToFirstIncident();
            MockDestinationTab();
            string text = CheckSaveState("Mensaje de estado", "Se guardaron los cambios exitosamente.");
            Assert.Equal("Se guardaron los cambios exitosamente.", text);
        }

        [Fact]
        public void FillInfoPatientTab()
        {
            driver = new ChromeDriver();
            AccessToIncidentPage();
            AccessToFirstIncident();
            MockPatientTab();
            string text = CheckSaveState("Mensaje de estado", "Se guardaron los cambios exitosamente.");
            Assert.Equal("Se guardaron los cambios exitosamente.", text);
        } 

        [Fact]
        public void ApproveIncident()
        {
            driver = new ChromeDriver();
            AccessToIncidentPage();
            AccessToFirstIncident();
            MockChangeState();
            Element = TryToFindById("Mensaje de estados");
            Assert.Equal("Aprobado por Teodoro Barquero ", Element.Text);
        }

        private void MockOriginTab()
        {
            Element = TryToFind("//div[@class='row']/div/ul/li[2]");//Origin tab
            Element.Click();
            Timeout(1000);//Load tab
            Element = TryToFindById("Tipo");//Dropdown menu tipo de origen
            oSelect = new SelectElement(Element);
            oSelect.SelectByIndex(1);//Select Internacional en el dropdown menu
            Element = TryToFindById("Pais");
            oSelect = new SelectElement(Element);
            oSelect.SelectByIndex(1);//Select El salvador en el dropdown menu
            Element = TryToFindById("Guardar");//Find guardar button
            Element.Click();
        }

        private void MockDestinationTab()
        {
            Element = TryToFind("//div[@class='row']/div/ul/li[3]");//Destination tab
            Element.Click();
            Timeout(1000);//Load tab
            Element = TryToFindById("Centro");//Dropdown menu Centro Medico
            oSelect = new SelectElement(Element);
            oSelect.SelectByIndex(2);//Select Centro Nacional de Rehabilitacion en el dropdown menu
            Element = TryToFindById("Medico");
            Timeout(1000);//Load Medicos
            oSelect = new SelectElement(Element);
            oSelect.SelectByIndex(1);//Select Wilbert Lopez en el dropdown menu
            Element = TryToFindById("Guardar");//Find guardar button
            Element.Click();
        }


        private void MockPatientTab()
        {
            Element = TryToFind("//div[@class='row']/div/ul/li[4]");//Patient tab
            Element.Click();
            Timeout(1000);//Load tab
            Element = TryToFindById("Cedula");//Input cedula
            Element.Clear();
            Element.SendKeys("111111111");//Mock
            Element = TryToFindById("Nombre");//Input Nombre
            Element.Clear();
            Element.SendKeys("Daniel");//Mock
            Element = TryToFindById("PrimerAp");//Input Primer Apellido
            Element.Clear();
            Element.SendKeys("Salazar");//Mock
            Element = TryToFindById("Guardar");//Input Nombre
            Element.Click();
        }


        private void MockChangeState()
        {
            Element = TryToFind("//div[@class='row']/div/ul/li[1]");//Details tab
            Element.Click();
            Timeout(1000);//Load tab
            Element = TryToFindById("Aprobado");
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
        
        private string CheckSaveState(string ElementId, string MsgToCompare)
        {
            IWebElement _element = TryToFindById(ElementId);
            string SavedMsg = "";
            bool exit = false;
            //OpenQA.Selenium.StaleElementReferenceException
            while (!exit)
            {
                exit = true;
                try
                {
                    SavedMsg = _element.Text;
                    if(SavedMsg != MsgToCompare)
                    {
                        exit = false;
                    }
                }
                catch (OpenQA.Selenium.StaleElementReferenceException e)
                {
                    _element = TryToFindById(ElementId);
                    exit = false;
                }
            }
            return SavedMsg;
        }

        private void AccessToIncidentPage()
        {
            //Wait time, TODO: search if its needed
            //_ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = IncidentsUrl;//Go to page
            driver.Manage().Window.Maximize();
            Element = TryToFindById("Correo");
            Element.Clear();
            Element.SendKeys("teodoro.barquero@prime.com");//Mock
            Element = TryToFindById("Contrasena");
            Element.Clear();
            Element.SendKeys("Teodoro.Barquero10");//Mock
            driver.FindElement(By.Id("Ingresar")).Click();//Ingresar página
        }
    }
}
