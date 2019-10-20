using Auden2.Drivers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Auden2.Logs
{
    [Binding]
    class ScreenShots
    {
        private readonly WebDriver _webDriver;

        public ScreenShots(WebDriver webDriver)
        {
            _webDriver = webDriver;

        }

        [AfterStep()]
        public void MakeScreenshotAfterStep()
        {
            var takesScreenshot = _webDriver.Current as ITakesScreenshot;
            if (takesScreenshot != null)
            {
                var screenshot = takesScreenshot.GetScreenshot();
                var tempFileName = Directory.GetCurrentDirectory() + _webDriver._scenario +".jpg";
                screenshot.SaveAsFile(tempFileName, ScreenshotImageFormat.Jpeg);
            }
        }
    }
}
