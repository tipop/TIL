using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotepadTest;
using System.Threading;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;

namespace TestScript1
{
    [TestClass]
    public class ScenarioPopupDialog : NotepadSession
    {
        private const string ExplorerAppId = @"C:\Windows\System32\explorer.exe";
        private const string TestFileName = "NotepadTestFile";
        private const string TargetSaveLocation = @"%TEMP%\";


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Create a Windows Explorer session to delete the saved text file above
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", ExplorerAppId);
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            var windowsExplorerSession = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);
            Assert.IsNotNull(windowsExplorerSession);

            // Navigate Windows Explorer to the target save location folder
            windowsExplorerSession.Keyboard.SendKeys(Keys.Alt + "d" + Keys.Alt + SanitizeBackslashes(TargetSaveLocation) + Keys.Enter);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            // Verify that the file is indeed saved in the working directory and delete it
            windowsExplorerSession.FindElementByAccessibilityId("SearchEditBox").SendKeys(TestFileName + Keys.Enter);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            WindowsElement testFileEntry = null;

            try
            {
                testFileEntry = windowsExplorerSession.FindElementByName("Items View").
                    FindElementByName(TestFileName + ".txt") as WindowsElement; // In case extension is added automatically
            }
            catch
            {
                try
                {
                    testFileEntry = windowsExplorerSession.FindElementByName("Items View").
                        FindElementByName(TestFileName) as WindowsElement;
                }
                catch { }
            }

            // Delete the test file when it exists
            if (testFileEntry != null)
            {
                testFileEntry.Click();
                testFileEntry.SendKeys(Keys.Delete);
                windowsExplorerSession.FindElementByName("Delete File").FindElementByName("Yes").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            windowsExplorerSession.Quit();
            windowsExplorerSession = null;
            TearDown();
        }

        [TestMethod]
        public void PopupDialogSaveFile()
        {
            ourSession.FindElementByName("File").Click();
            ourSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Save As\")]").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));  // Wait for 1 second until the save dialog appears

            ourSession.FindElementByAccessibilityId("FileNameControlHost").SendKeys(SanitizeBackslashes(TargetSaveLocation + TestFileName));
            ourSession.FindElementByName("Save").Click();

            // Check if the Save As dialog appears when there's a leftover test file from previous test run
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));  // Wait for 1 second in case save as dialog appears
                ourSession.FindElementByName("Confirm Save As").FindElementByName("Yes").Click();
            }
            catch { }

            // Verify that Notepad has saved the edited text file with the given name
            Thread.Sleep(TimeSpan.FromSeconds(1.5));    // Wait for 1.5 seconds until the window title is updated
            Assert.IsTrue(ourSession.Title.Contains(TestFileName));
        }
    }
}
