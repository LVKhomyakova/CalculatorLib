using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLib;
using System.Collections;
using System.Collections.Generic;

namespace CalculatorLibTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestSummarize()
        {
            Assert.AreEqual(9, Calculator.Summarize(6.0, 3));
        }

        [TestMethod]
        public void TestSubtract()
        {
            Assert.AreEqual(3, Calculator.Subtract(6.0, 3));
        }

        [TestMethod]
        public void TestMultiply()
        {
            Assert.AreEqual(18, Calculator.Multiply(6.0, 3));
        }

        [TestMethod]
        public void TestDivide()
        {
            Assert.AreEqual(2, Calculator.Divide(6.0, 3));
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            Assert.AreEqual(0, Calculator.Divide(6.0, 0));
        }

        [TestMethod]
        public void TestLogarithm10()
        {
            Assert.AreEqual(3, Calculator.Logarithm10(1000));
        }

        [TestMethod]
        public void TestSquareRoot()
        {
            Assert.AreEqual(5, Calculator.SquareRoot(25));
        }

        [TestMethod]
        public void TestPower()
        {
            Assert.AreEqual(8, Calculator.Power(2,3));
        }

        [TestMethod]
        public void TestCalculateExpression_NoBrackets()
        {
            double resultExpected = 8;
            double resultActual = Calculator.CalculateExpression("5*2-10/5");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [TestMethod]
        public void TestCalculateExpression_Brackets()
        {
            double resultExpected = 12;
            double resultActual = Calculator.CalculateExpression("(5*(22-10)/5.0)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [TestMethod]
        public void TestCalculateExpression_NegativeInCalculation()
        {
            double resultExpected = -8;
            double resultActual = Calculator.CalculateExpression("(5*(2-10)/5.0)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [TestMethod]
        public void TestCalculateExpression_NegativeInExpression()
        {
            double resultExpected = 8;
            double resultActual = Calculator.CalculateExpression("(5*2-(-10)/(-5.0))");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [TestMethod]
        public void TestCalculateExpression_OneOperand()
        {
            double resultExpected = 2;  //5*2-(-10)/lg(105-5)*sqrt(25)
            double resultActual = Calculator.CalculateExpression("lg(105-5)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [TestMethod]
        public void TestCalculateExpression_AllInclude()
        {
            double resultExpected = -8.65942655433;  //   
            double resultActual = Calculator.CalculateExpression("lg(8)^(-5)+85/19.8*lg(lg(sqrt(9)))*sqrt(56)");
            Assert.AreEqual(resultExpected, resultActual,1e-10);
        }

        [TestMethod]
        public void TestParseExpression()
        {
            List<string> resultExpected =new List<string> {"(","5","*","(","2","-","10",")","/","5.0",")" };
            List<string> resultActual = Calculator.ParseExpression("(5*(2-10)/5.0)");
            for(int i=0;i<resultExpected.Count;i++)
            {
                Assert.AreEqual(resultExpected[i], resultActual[i]);
            }
        }
    }
}
