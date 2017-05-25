using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trailmaker.UI;

namespace Trailmaker.UI.UnitTests
{
    [TestFixture()]
    public class FramePaddingCalculatorTests
    {
        [Test()]
        public void CalculatePaddingXOffsetPositive()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(10, 0, 0, 0);

            Assert.AreEqual(0, padding.Left);
            Assert.AreEqual(0, padding.Top);
            Assert.AreEqual(10, padding.Right);
            Assert.AreEqual(0, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingXOffsetNegative()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(-10, 0, 0, 0);

            Assert.AreEqual(10, padding.Left);
            Assert.AreEqual(0, padding.Top);
            Assert.AreEqual(0, padding.Right);
            Assert.AreEqual(0, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingYOffsetPositive()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(0, 10, 0, 0);

            Assert.AreEqual(0, padding.Left);
            Assert.AreEqual(0, padding.Top);
            Assert.AreEqual(0, padding.Right);
            Assert.AreEqual(10, padding.Bottom);
        }

        [Test()]
        public void CacluatePaddingYOffsetNegative()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(0, -10, 0, 0);

            Assert.AreEqual(0, padding.Left);
            Assert.AreEqual(10, padding.Top);
            Assert.AreEqual(0, padding.Right);
            Assert.AreEqual(0, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingSigmaX()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(0, 0, 10, 0);

            Assert.AreEqual(20, padding.Left);
            Assert.AreEqual(0, padding.Top);
            Assert.AreEqual(20, padding.Right);
            Assert.AreEqual(0, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingSigmaY()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(0, 0, 0, 10);

            Assert.AreEqual(0, padding.Left);
            Assert.AreEqual(20, padding.Top);
            Assert.AreEqual(0, padding.Right);
            Assert.AreEqual(20, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingXSigmaGreaterThanOffset()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(5, 0, 10, 0);

            Assert.AreEqual(15, padding.Left);
            Assert.AreEqual(0, padding.Top);
            Assert.AreEqual(25, padding.Right);
            Assert.AreEqual(0, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingYSigmaGreaterThanOffset()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(0, 5, 0, 10);

            Assert.AreEqual(0, padding.Left);
            Assert.AreEqual(15, padding.Top);
            Assert.AreEqual(0, padding.Right);
            Assert.AreEqual(25, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingXSigmaLessThanOffset()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(20, 0, 5, 0);

            Assert.AreEqual(0, padding.Left);
            Assert.AreEqual(0, padding.Top);
            Assert.AreEqual(30, padding.Right);
            Assert.AreEqual(0, padding.Bottom);
        }

        [Test()]
        public void CalculatePaddingYSigmaLessThanOffset()
        {
            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(0, 20, 0, 5);

            Assert.AreEqual(0, padding.Left);
            Assert.AreEqual(0, padding.Top);
            Assert.AreEqual(0, padding.Right);
            Assert.AreEqual(30, padding.Bottom);
        }
    }
}
