using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace CalculatorTest
{
    public class CalculatorSession
    {
        protected const string WinAppDriverUrl = "http://127.0.0.1:4723/";
        private const string CalculatorAppId = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";

        protected static WindowsDriver<WindowsElement> ourSession;

        public static void Setup(TestContext context)
        {
            // Launch Calculator application if it is not yet launched
            if (ourSession == null)
            {
                var options = new AppiumOptions();
                options.AddAdditionalCapability("app", CalculatorAppId);
                options.AddAdditionalCapability("deviceName", "WindowsPC");
                ourSession = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);

                Assert.IsNotNull(ourSession);
                Assert.IsNotNull(ourSession.SessionId);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times.
                ourSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (ourSession != null)
            {
                ourSession.Quit();
                ourSession = null;
            }
        }
    }
}
