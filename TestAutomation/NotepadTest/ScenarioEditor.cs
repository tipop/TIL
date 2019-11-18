using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;

namespace NotepadTest
{
    [TestClass]
    public class ScenarioEditor : NotepadSession
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
        public void EditorEnterText()
        {
            // Type mixed text and apply shift modifier to 7890_ to generate corresponding symbols.
            Thread.Sleep(TimeSpan.FromSeconds(2));
            ourEditbox.SendKeys("abcdeABCDE 12345" + Keys.Shift + "7890-" + Keys.Shift + @"!@#$%");
            Assert.AreEqual(@"abcdeABCDE 12345&*()_!@#$%", ourEditbox.Text);
        }

        [TestMethod]
        public void EditorKeyboardShortcut()
        {
            // Type a known sequence, select, copy, paste it there times.
            ourEditbox.SendKeys("789");
            ourEditbox.SendKeys(Keys.Control + "a" + Keys.Control); // Select all using Ctrl + A keyboard shortcut
            ourEditbox.SendKeys(Keys.Control + "c" + Keys.Control); // Copy using Ctrl + C keyboard shortcut
            ourEditbox.SendKeys(Keys.Control + "vvv" + Keys.Control); // Paste 3 times using Ctrl + V keyboard shortcut

            Assert.AreEqual("789789789", ourEditbox.Text);
        }

        [TestMethod]
        public void EditorNonPrintableShortcutKey()
        {
            // Press F5 to get Time/Date from Notepad
            Assert.AreEqual(string.Empty, ourEditbox.Text);
            ourEditbox.SendKeys(Keys.F5);
            Assert.AreNotEqual(string.Empty, ourEditbox.Text);
        }
    }
}
