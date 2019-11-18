using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;

namespace CalculatorTest
{
    [TestClass]
    public class ScenarioStandard : CalculatorSession
    {
        private static WindowsElement header;
        private static WindowsElement calculatorResult;

        [TestMethod]
        public void Addition()
        {
            // Find the buttons by their names and click them in sequence to perform 1 + 7 = 8
            ourSession.FindElementByAccessibilityId("num1Button").Click();
            ourSession.FindElementByAccessibilityId("plusButton").Click();
            ourSession.FindElementByAccessibilityId("num7Button").Click();
            ourSession.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        public void Division()
        {
            // Find the buttons by their accessibility ids and click them in sequence to perform 88 / 11 = 8
            ourSession.FindElementByAccessibilityId("num8Button").Click();
            ourSession.FindElementByAccessibilityId("num8Button").Click();
            ourSession.FindElementByAccessibilityId("divideButton").Click();
            ourSession.FindElementByAccessibilityId("num1Button").Click();
            ourSession.FindElementByAccessibilityId("num1Button").Click();
            ourSession.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        public void Multiplication()
        {
            // Find the buttons by their names using XPath and click them in sequence to perform 9 x 9 = 81
            ourSession.FindElementByXPath("//Button[@AutomationId='num9Button']").Click();
            ourSession.FindElementByXPath("//Button[@AutomationId='multiplyButton']").Click();
            ourSession.FindElementByXPath("//Button[@AutomationId='num9Button']").Click();
            ourSession.FindElementByXPath("//Button[@AutomationId='equalButton']").Click();
            Assert.AreEqual("81", GetCalculatorResultText());
        }

        [TestMethod]
        public void Subtraction()
        {
            // Find the buttons by their accessibility ids using XPath and click them in sequence to perform 9 - 1 = 8
            ourSession.FindElementByXPath("//Button[@AutomationId=\"num9Button\"]").Click();
            ourSession.FindElementByXPath("//Button[@AutomationId=\"minusButton\"]").Click();
            ourSession.FindElementByXPath("//Button[@AutomationId=\"num1Button\"]").Click();
            ourSession.FindElementByXPath("//Button[@AutomationId=\"equalButton\"]").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch a Calculator window
            Setup(context);

            // Identify calculator mode by locating the header
            try
            {
                header = ourSession.FindElementByAccessibilityId("Header");
            }
            catch
            {
                header = ourSession.FindElementByAccessibilityId("ContentPresenter");
            }

            // Ensure that calculator is in standard mode
            if (!header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase))
            {
                ourSession.FindElementByAccessibilityId("TogglePaneButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));

                var splitViewPane = ourSession.FindElementByClassName("SplitViewPane");
                splitViewPane.FindElementByAccessibilityId("Standard").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Assert.IsTrue(header.Text.Equals("표준", StringComparison.OrdinalIgnoreCase));
            }

            calculatorResult = ourSession.FindElementByAccessibilityId("CalculatorResults");
            Assert.IsNotNull(calculatorResult);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestInitialize]
        public void Clear()
        {
            ourSession.FindElementByAccessibilityId("clearButton").Click();
            string result = GetCalculatorResultText();
            Assert.AreEqual("0", result);
        }

        private string GetCalculatorResultText()
        {
            return calculatorResult.Text.Replace("표시는 ", string.Empty).Trim();
        }
    }
}
