using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace NotepadTest
{
    public class NotepadSession
    {
        protected const string WinAppDriverUrl = "http://127.0.0.1:4723/";
        private const string NotepadAppId = @"C:\Windows\Syswow64\notepad.exe";

        protected static WindowsDriver<WindowsElement> ourSession;
        protected static WindowsElement ourEditbox;

        public static void Setup(TestContext context)
        {
            // Launch a new instance of Notepad application
            if (ourSession == null)
            {
                // Create a new session to launch Notepad application
                var options = new AppiumOptions();
                options.AddAdditionalCapability("app", NotepadAppId);
                options.AddAdditionalCapability("deviceName", "WindowsPC");
                ourSession = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);

                Assert.IsNotNull(ourSession);
                Assert.IsNotNull(ourSession.SessionId);

                // Verify that Notepad is started with untitled new file
                Assert.AreEqual("Untitled - Notepad", ourSession.Title);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times.
                ourSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                // Keep track of the edit box to be used throughout the session
                ourEditbox = ourSession.FindElementByClassName("Edit");
                Assert.IsNotNull(ourEditbox);
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (ourSession != null)
            {
                ourSession.Close();

                try
                {
                    // Dismiss Save dialog if it is blocking the exit
                    ourSession.FindElementByName("Don't Save").Click();
                }
                catch { }

                ourSession.Quit();
                ourSession = null;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Select all text and delete to clear the edit box
            ourEditbox.SendKeys(Keys.Control + "a" + Keys.Control);
            ourEditbox.SendKeys(Keys.Delete);
            Assert.AreEqual(string.Empty, ourEditbox.Text);
        }

        protected static string SanitizeBackslashes(string input) => input.Replace("\\", Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt);
    }
}
