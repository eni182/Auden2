using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using FluentAssertions;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Diagnostics;
using Auden2.Drivers;

namespace Auden2
{
    [Binding]
    public class LoanAmountScrollerSteps
    {
        private readonly WebDriver _webDriver;
        IWebDriver driver;
        Random rnd = new Random();


        public LoanAmountScrollerSteps(WebDriver webDriver)
        {
            _webDriver = webDriver;
            driver = _webDriver.Current;
        }

        [Given(@"Slider Default Loan Amount Amount")]
        public void GivenSliderDefaultLoanAmountAmount()
        {
            _webDriver.Scenario("GivenSliderDefaultLoanAmountIs");
            Thread.Sleep(2000);

            var defaultLoanAmount = driver.FindElement(By.XPath("//span[contains(text(),'£400')]"));
            var defaultLoanAmountString = defaultLoanAmount.Text.ToString();

            defaultLoanAmountString.Should().Contain("400");
        }

        [When(@"Move slider in any direction")]
        public void WhenMoveSliderInAnyDirection()
        {
            _webDriver.Scenario("WhenMoveSliderInAnyDirection");
            Thread.Sleep(2000);


            var ranNumber = rnd.Next(200, 1000);
            var Slider = driver.FindElement(By.XPath("//input[@name='amount']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value='" + ranNumber.ToString() + "';", Slider);

            Thread.Sleep(1000);
            Actions actions = new Actions(driver);
            actions.ContextClick(Slider).Perform();
            Thread.Sleep(1000);

            //js.ExecuteScript("arguments[0].click();", Slider);

            var ranNumberRoundedUp = ((int)Math.Round(ranNumber / 10.0)) * 10;


            var outcome = driver.FindElement(By.XPath("//span[contains(text(),'£" + ranNumberRoundedUp.ToString() + "')]"));
            outcome.Text.Should().Contain(ranNumberRoundedUp.ToString());
        }

        [When(@"Minimum amount should be")]
        public void WhenMinimumAmountShouldBe()
        {

            _webDriver.Scenario("WhenMinimumAmountShouldBe");
            Thread.Sleep(2000);


            var minimum = ConfigurationManager.AppSettings["MinimumLoanAmount"];
            var Slider = driver.FindElement(By.XPath("//input[@name='amount']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value='0';", Slider);

            Thread.Sleep(1000);
            Actions actions = new Actions(driver);
            actions.ContextClick(Slider).Perform();
            Thread.Sleep(1000);


            var outcome = driver.FindElement(By.XPath("//span[contains(text(),'£" + minimum.ToString() + "')]"));

            outcome.Text.Should().Contain(minimum);
        }
        
        [When(@"Maximum amount should be")]
        public void WhenMaximumAmountShouldBe()
        {
            _webDriver.Scenario("WhenMaximumAmountShouldBe");
            Thread.Sleep(2000);

            var maximum = ConfigurationManager.AppSettings["MaximumLoanAmount"];
            var Slider = driver.FindElement(By.XPath("//input[@name='amount']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value='10000';", Slider);

            Thread.Sleep(1000);
            Actions actions = new Actions(driver);
            actions.ContextClick(Slider).Perform();
            Thread.Sleep(1000);


            var outcome = driver.FindElement(By.XPath("//span[contains(text(),'£" + maximum.ToString() + "')]"));

            outcome.Text.Should().Contain(maximum);
        }
    }
}
