using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TechTalk.SpecFlow;

namespace Auden2.Drivers
{

    public enum DriverToUse
    {
            InternetExplorer,
            Chrome,
            Firefox
    }

    public class WebDriver
    {


        private IWebDriver driver;
        private WebDriverWait _wait;
        public string _scenario;


        public void Scenario(string scenario)
        {
            _scenario = scenario;

        }

        public IWebDriver Current
        {
            get
            {
                if (driver == null)
                {
                    driver = Create();
                }

                return driver;

            }
        }

        public WebDriverWait Wait
        {
            get
            {
                if (_wait == null)
                {
                    this._wait = new WebDriverWait(Current, TimeSpan.FromSeconds(10));
                }
                return _wait;
            }
        }

        private static FirefoxOptions FirefoxOptions
        {
            get
            {

                var firefoxProfile = new FirefoxOptions();

                firefoxProfile.SetPreference("network.automatic-ntlm-auth.trusted-uris", "http://localhost");
                return firefoxProfile;
            }
        }

        public IWebDriver Create()
        {

            var driverToUse = ConfigurationManager.AppSettings["DriverToUse"];

            switch (driverToUse)
            {
                case "InternetExplorer":
                    driver = new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory, new InternetExplorerOptions{ IgnoreZoomLevel = true }, TimeSpan.FromMinutes(5));
                    break;
                case "Firefox":
                    var firefoxProfile = FirefoxOptions;
                    driver = new FirefoxDriver(firefoxProfile);
                    driver.Manage().Window.Maximize();
                    break;
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

                    /*Currently need to ensure naming is correct in App.config
                    * Login will need updating */
                        
            }

            driver.Manage().Window.Maximize();
            var timeouts = driver.Manage().Timeouts();

            var implicitWait = ConfigurationManager.AppSettings["ImplicitlyWait"];
            var pageLoadTimeout = ConfigurationManager.AppSettings["PageLoadTimeout"];

            int x = 0;
            int y = 0;

            Int32.TryParse(implicitWait, out x);
            Int32.TryParse(pageLoadTimeout, out y);

            timeouts.ImplicitWait = TimeSpan.FromSeconds(x);
            timeouts.PageLoad = TimeSpan.FromSeconds(y);

            // Suppress the onbeforeunload event first. This prevents the application hanging on a dialog box that does not close.
            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            return driver;
        }

    }

}
