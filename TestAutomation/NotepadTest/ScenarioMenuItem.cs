using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace NotepadTest
{
    [TestClass]
    public class ScenarioMenuItem : NotepadSession
    {
        [ClassInitialize]
        public static void ClassInitilaize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestMethod]
        public void MenuItemEdit()
        {
            // Select Edit -> Time/Date to get Time/Date from Notepad
            Assert.AreEqual(string.Empty, ourEditbox.Text);

            WindowsElement editMenu = ourSession.FindElementByName("Edit");
            editMenu.Click();
            ourSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Time/Date\")]").Click();
            string timeDateString = ourEditbox.Text;
            Assert.AreNotEqual(string.Empty, timeDateString);

            // Select all text, copy, and paste it twice using MenuItem Edit
            // -> Select All, Copy, and Paste
            editMenu.Click();
            ourSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Select All\")]").Click();

            editMenu.Click();
            ourSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Copy\")]").Click();

            editMenu.Click();
            ourSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Paste\")]").Click();

            editMenu.Click();
            ourSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Paste\")]").Click();

            // Verify that the Time/Date string is duplicated
            Assert.AreEqual(timeDateString + timeDateString, ourEditbox.Text);
        }
    }
}
