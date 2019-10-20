using Auden2.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using FluentAssertions;
using System.Configuration;
using System.Threading;
using TechTalk.SpecFlow;

namespace Auden2.FeatureFiles
{
    [Binding]
    public class CheckRepaymentDateSteps
    {
        private readonly WebDriver _webDriver;
        IWebDriver driver;

        public CheckRepaymentDateSteps(WebDriver webDriver)
        {
            _webDriver = webDriver;
            driver = _webDriver.Current;
        }


        [When(@"Repayment Date is selected, Check Tomake sure it is Valid")]
        public void WhenRepaymentDateIsSelectedCheckTomakeSureItIsValid()
        {
            _webDriver.Scenario("WhenTheRepaymentDayIsOnWeekdays");
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            var ExpandButton = driver.FindElement(By.XPath("//div[@class='loan-schedule__tab__panel']"));
            //js.ExecuteScript("arguments[0].scrollIntoView(true);", ExpandButton);
            js.ExecuteScript("window.scroll(0, 250);", "");
            Thread.Sleep(1000);


            Actions actions = new Actions(driver);
            actions.Click(ExpandButton).Perform();
            Thread.Sleep(3000);

            var Calender = driver.FindElement(By.XPath("//div[@class='date-selector']"));
            js.ExecuteScript("arguments[0].click();", Calender);
            Thread.Sleep(3000);

            var configRepaymentDate = ConfigurationManager.AppSettings["RepaymentDate"];
            Int32.TryParse(configRepaymentDate, out int x);
            int configRepaymentDateNumber = x;



            IWebElement RepaymentDate = null;
            int year = DateTime.Now.Year;
            int nextMonth = DateTime.Now.Month + 1;
            if (nextMonth > 12)
                nextMonth = 1;

            int DaysinTheMonth = DateTime.DaysInMonth(year, nextMonth);
            int CurrentDay = DateTime.Now.Day;




            configRepaymentDateNumber.Should().BeInRange(1, DaysinTheMonth);
            configRepaymentDateNumber.Should().BeLessOrEqualTo(CurrentDay);

            try
            {
                RepaymentDate = driver.FindElement(By.XPath("//span[" + configRepaymentDate + "]//button[1]"));
            }
            catch
            {
                RepaymentDate = driver.FindElement(By.XPath("//button[contains(text(),'" + configRepaymentDate + "')]"));

            }

            RepaymentDate.Click();
            //  js.ExecuteScript("arguments[0].click();", Calender);
            Thread.Sleep(3000);

            DateTime returndate = new DateTime(year, nextMonth, configRepaymentDateNumber);

            var displayedRepaymentDate = driver.FindElement(By.XPath("//span[@class='loan-schedule__tab__panel__detail__tag__text']"));
            var expectedRepaymentDate = returndate.ToString("dddd d MMM yyyy");

            displayedRepaymentDate.Text.Should().NotContain("Saturday");
            displayedRepaymentDate.Text.Should().NotContain("Sunday");


            if ((returndate.DayOfWeek == DayOfWeek.Saturday || returndate.DayOfWeek == DayOfWeek.Sunday))
            {
                displayedRepaymentDate.Text.Should().NotContain(expectedRepaymentDate);

            }
            else
            {
                displayedRepaymentDate.Text.Should().BeEquivalentTo(expectedRepaymentDate);

            }


        }
    }
}
