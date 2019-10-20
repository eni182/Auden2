using Auden2.Drivers;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
namespace Auden2.FeatureFiles
{
    [Binding]
    public class AudenWebsiteSteps
    {

        private readonly WebDriver _webDriver;
        IWebDriver driver;


        public AudenWebsiteSteps(WebDriver webDriver)
        {
            _webDriver = webDriver;
            driver = _webDriver.Current;
        }

        [BeforeScenario]
        public void Browser()
        {
            driver.Manage().Window.Maximize();
            string baseUrl = ConfigurationManager.AppSettings["TargetUrl"];
            driver.Navigate().GoToUrl(baseUrl);
        }

        [AfterScenario]
        public void QuitBrowser()
        {
            driver.Quit();
        }


    }
}
