
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TalkCalc.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void NullStringShouldEvaluateToZero()
        {
            test(null, 0F);
            test(string.Empty, 0F);
        }

        [TestMethod]
        public void SimpleDigitShouldEvaluateToSelf()
        {
            for (var i = 0; i < 10; i++)
                test(i.ToString(), (float)i);
        }

        [TestMethod]
        public void NumbersShouldEvaluateToSelf()
        {
            for (var i = 1; i < 10000; i *= 2)
                test(i.ToString(), (float)i);
        }

        [TestMethod]
        public void SingleOperationShouldEvaluateCorrectly()
        {
            test("1 + 1", 2F);
            test("98 - 89", -9F);
            test("7 * 7", 49F);
            test("888 / 888", 1F);
            test("9 - 5", -4F);
        }

        [TestMethod]
        public void MultipleOperationShouldEvaluateInCorrectOrder()
        {
            test("4 + 5 * 6", 34F);
            test("5 * 6 + 4", 34F);
        }




        private void test(string expr, float expectedResult)
        {
            var calc = new Calculator.Calculator();
            calc.Expression = expr;

            var result = calc.Calculate();
            Assert.AreEqual(result, calc.Result);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
