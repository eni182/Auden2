using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Configuration;
using Auden2.Drivers;
using System.Threading;

namespace Auden2
{
    [Binding]
    public class LoanDurationSteps
    {
        private readonly WebDriver _webDriver;
        IWebDriver driver;


        public LoanDurationSteps(WebDriver webDriver)
        {
            _webDriver = webDriver;
            driver = _webDriver.Current;
        }


        [When(@"When Mohtly Loan Duration is Selected")]
        public void WhenWhenMohtlyLoanDurationIsSelected()
        {
            _webDriver.Scenario("WhenWhenMohtlyLoanDurationIsSelected");

            try
            {
                driver.FindElement(By.XPath("//nav[@class='tab-list']//button[2]")).Click();
            }
            catch (Exception e)
            {

            }

            var maximumLoanDuration = driver.FindElement(By.XPath("//span[contains(text(),'2 -')]"));
            var maximumLoanDurationString = maximumLoanDuration.Text.ToString();

            maximumLoanDurationString.Should().Contain("12");

            Thread.Sleep(2000);

        }

    }
}
