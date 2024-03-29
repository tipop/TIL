﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.Interactions;

namespace PaintTest
{
    [TestClass]
    public class ScenarioDraw : PaintSession
    {
        private WindowsElement inkCanvas;
        private WindowsElement undoButton;

        private const int defaultRadius = 300;  // half of ABC square side. E.g. distance between AB
        private const int radiusOffset = 10;    // distance between concetric square in pixels

        [TestMethod]
        public void DrawConcentricSquaresWithVaryingDuration()
        {
            const int concentricSquareCount = 8;    // Paint application only supports up to 10 touch inputs
            List<ActionSequence> actionSequencesList = new List<ActionSequence>();

            // Draw N concentric rectangles with varying speed defined by the duration specified in durationMs
            for (int i = 0, radius = defaultRadius, durationMs = 1000; i < concentricSquareCount && radius > 0; i++, radius -= radiusOffset)
            {
                OpenQA.Selenium.Appium.Interactions.PointerInputDevice touchContact = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
                ActionSequence touchSequence = new ActionSequence(touchContact, 0);
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, -radius, -radius, TimeSpan.Zero));
                touchSequence.AddAction(touchContact.CreatePointerDown(PointerButton.TouchContact));
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, radius, -radius, TimeSpan.FromMilliseconds(durationMs)));
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, radius, radius, TimeSpan.FromMilliseconds(durationMs)));
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, -radius, radius, TimeSpan.FromMilliseconds(durationMs)));
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, -radius, -radius, TimeSpan.FromMilliseconds(durationMs)));
                touchSequence.AddAction(touchContact.CreatePointerUp(PointerButton.TouchContact));
                actionSequencesList.Add(touchSequence);
                durationMs += 300;
            }

            ourSession.PerformActions(actionSequencesList);

            // Verify that the drawing operations took place
            Assert.IsTrue(undoButton.Displayed);
            Assert.IsTrue(undoButton.Enabled);
        }

        [TestMethod]
        public void DrawFlower()
        {
            const int LinesCount = 10; // Paint application only supports up to 10 touch inputs
            List<ActionSequence> actionSequencesList = new List<ActionSequence>();

            // Draw N petals shape graph to create a flower like shape
            double angle = 0;
            for (int line = 0; line < LinesCount; line++)
            {
                OpenQA.Selenium.Appium.Interactions.PointerInputDevice touchContact = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
                ActionSequence touchSequence = new ActionSequence(touchContact, 0);

                int radius = 0;
                int x = (int)(Math.Sin(angle) * (defaultRadius * Math.Cos(radius * 2 * Math.PI / 360)));
                int y = (int)(Math.Cos(angle) * (defaultRadius * Math.Cos(radius * 2 * Math.PI / 360)));
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, x, -y, TimeSpan.Zero));
                touchSequence.AddAction(touchContact.CreatePointerDown(PointerButton.TouchContact));

                double subAngle = angle;
                while (radius < defaultRadius)
                {
                    x = (int)(Math.Sin(subAngle) * (defaultRadius * Math.Cos(radius * 2 * Math.PI / 360)));
                    y = (int)(Math.Cos(subAngle) * (defaultRadius * Math.Cos(radius * 2 * Math.PI / 360)));
                    touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, x, -y, TimeSpan.Zero));
                    radius += 5;
                    subAngle += 2 * Math.PI / 360;
                }

                touchSequence.AddAction(touchContact.CreatePointerUp(PointerButton.TouchContact));
                actionSequencesList.Add(touchSequence);
                angle += 2 * Math.PI / LinesCount;
            }

            ourSession.PerformActions(actionSequencesList);

            // Verify that the drawing operations took place
            Assert.IsTrue(undoButton.Displayed);
            Assert.IsTrue(undoButton.Enabled);
        }

        [TestMethod]
        public void DrawFlowerPetals()
        {
            const int LinesCount = 10; // Paint application only supports up to 10 touch inputs
            List<ActionSequence> actionSequencesList = new List<ActionSequence>();

            // Draw N petals shape graph to create a flower like shape
            double angle = 0;
            for (int line = 0; line < LinesCount; line++)
            {
                OpenQA.Selenium.Appium.Interactions.PointerInputDevice touchContact = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
                ActionSequence touchSequence = new ActionSequence(touchContact, 0);

                // Start at the center of the inkCanvas to create straight line in the middle of the petal
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, 0, 0, TimeSpan.Zero));
                touchSequence.AddAction(touchContact.CreatePointerDown(PointerButton.TouchContact));

                int radius = 100;
                double subAngle = angle;
                while (radius < defaultRadius)
                {
                    int x = (int)(Math.Sin(subAngle) * (defaultRadius * Math.Sin(radius * 2 * Math.PI / 360)));
                    int y = (int)(Math.Cos(subAngle) * (defaultRadius * Math.Sin(radius * 2 * Math.PI / 360)));
                    touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, x, -y, TimeSpan.Zero));
                    radius += 5;
                    subAngle += 2 * Math.PI / 360;
                }

                touchSequence.AddAction(touchContact.CreatePointerUp(PointerButton.TouchContact));
                actionSequencesList.Add(touchSequence);
                angle += 2 * Math.PI / LinesCount;
            }

            ourSession.PerformActions(actionSequencesList);

            // Verify that the drawing operations took place
            Assert.IsTrue(undoButton.Displayed);
            Assert.IsTrue(undoButton.Enabled);
        }

        [TestMethod]
        public void DrawHurricane()
        {
            const int LinesCount = 10; // Paint application only supports up to 10 touch inputs
            List<ActionSequence> actionSequencesList = new List<ActionSequence>();

            // Draw N lines following a rotating radial pattern ressembling a hurricane eye
            double angle = 0;
            for (int line = 0; line < LinesCount; line++)
            {
                OpenQA.Selenium.Appium.Interactions.PointerInputDevice touchContact = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
                ActionSequence touchSequence = new ActionSequence(touchContact, 0);

                int radius = 100;
                int x = (int)(Math.Sin(angle) * radius);
                int y = (int)(Math.Cos(angle) * radius);
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, x, -y, TimeSpan.Zero));
                touchSequence.AddAction(touchContact.CreatePointerDown(PointerButton.TouchContact));

                double subAngle = angle;
                while (radius < defaultRadius)
                {
                    x = (int)(Math.Sin(subAngle) * radius);
                    y = (int)(Math.Cos(subAngle) * radius);
                    touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, x, -y, TimeSpan.Zero));
                    radius += 5;
                    subAngle += 2 * Math.PI / 36;
                }

                touchSequence.AddAction(touchContact.CreatePointerUp(PointerButton.TouchContact));
                actionSequencesList.Add(touchSequence);
                angle += 2 * Math.PI / LinesCount;
            }

            ourSession.PerformActions(actionSequencesList);

            // Verify that the drawing operations took place
            Assert.IsTrue(undoButton.Displayed);
            Assert.IsTrue(undoButton.Enabled);
        }

        [TestMethod]
        public void DrawTwistingHurricane()
        {
            const int LinesCount = 10; // Paint application only supports up to 10 touch inputs
            List<ActionSequence> actionSequencesList = new List<ActionSequence>();

            // Draw N lines following a twisting & rotating radial pattern ressembling a hurricane eye
            double angle = 0;
            for (int line = 0; line < LinesCount; line++)
            {
                OpenQA.Selenium.Appium.Interactions.PointerInputDevice touchContact = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
                ActionSequence touchSequence = new ActionSequence(touchContact, 0);

                int radius = 100;
                int x = (int)(Math.Sin(angle) * radius);
                int y = (int)(Math.Cos(angle) * radius);
                touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, x, -y, TimeSpan.Zero));
                touchSequence.AddAction(touchContact.CreatePointerDown(PointerButton.TouchContact));

                double subAngle = angle;
                while (radius < defaultRadius)
                {
                    x = (int)(Math.Sin(subAngle) * radius);
                    y = (int)(Math.Cos(subAngle) * radius);
                    touchSequence.AddAction(touchContact.CreatePointerMove(inkCanvas, x, -y, TimeSpan.Zero));
                    radius += 5;
                    subAngle += 2 * Math.PI / 36;
                }

                touchSequence.AddAction(touchContact.CreatePointerUp(PointerButton.TouchContact));
                actionSequencesList.Add(touchSequence);
                angle += Math.PI / LinesCount;
            }

            ourSession.PerformActions(actionSequencesList);

            // Verify that the drawing operations took place
            Assert.IsTrue(undoButton.Displayed);
            Assert.IsTrue(undoButton.Enabled);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch or bring up Paint application
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestInitialize]
        public void SetupDrawingScenario()
        {
            // Locate the drawing surface
            inkCanvas = ourSession.FindElementByClassName("MSPaintView");

            // Locate the Undo button
            undoButton = ourSession.FindElementByName("Undo");
            undoButton.Click();

            Assert.IsTrue(undoButton.Displayed);
            Assert.IsFalse(undoButton.Enabled);
        }

    }
}
