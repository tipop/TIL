using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace PaintTest
{
    public class PaintSession
    {
        protected const string WinAppDriverUrl = "http://127.0.0.1:4723/";
        private const string PaintAppId = @"C:\Windows\Syswow64\mspaint.exe";

        protected static WindowsDriver<WindowsElement> ourSession;

        public static void Setup(TestContext context)
        {
            // Launch Paint 3D application if it is not yet launched
            if (ourSession == null)
            {
                var options = new AppiumOptions();
                options.AddAdditionalCapability("app", PaintAppId);
                options.AddAdditionalCapability("deviceName", "WindowsPC");
                ourSession = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);

                Assert.IsNotNull(ourSession);
                Assert.IsNotNull(ourSession.SessionId);

                // Verify that Notepad is started with untitled new file
                //Assert.AreEqual("Untitled - Notepad", ourSession.Title);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times.
                ourSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                //ourEditbox = ourSession.FindElementByClassName("Edit");
                //Assert.IsNotNull(ourEditbox);
            }
        }



        [TestInitialize]
        public void CreateNewPaintProject()
        {
            // Create a new Paint 3D project by pressing Ctrl + N
            ourSession.SwitchTo().Window(ourSession.CurrentWindowHandle);
            ourSession.Keyboard.SendKeys(Keys.Control + "n" + Keys.Control);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            DismissSaveConfirmDialog();
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (ourSession != null)
            {
                ClosePaint();
                ourSession.Quit();
                ourSession = null;
            }
        }

        private static void ClosePaint()
        {
            try
            {
                ourSession.Close();
                string currentHandle = ourSession.CurrentWindowHandle;

                DismissSaveConfirmDialog();
            }
            catch { }
        }

        private static void DismissSaveConfirmDialog()
        {
            try
            {
                ourSession.FindElementByName("Don't Save").Click();
            }
            catch { }
        }
    }
}
