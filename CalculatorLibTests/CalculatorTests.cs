﻿using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLib;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace CalculatorLibTests
{
    [TestFixture]
    public class CalculatorTests
    {
        static double[][] SumCases = new double[][] {
            new double[]{ 5.5, 3, 8.5 },
            new double[]{ 0.0, -3, -3 },
            new double[]{ 5, 0, 5 },
            new double[]{ -6.0, -3, -9 } };

        [TestCaseSource("SumCases")]
        public void Test_Sum(double a, double b, double expRes)
        {
            Assert.AreEqual(expRes, Calculator.Sum(a, b));
        }

        [TestCase(5.5, 3, 2.5)]
        [TestCase(0.0, -3, 3)]
        [TestCase(5, 0, 5)]
        [TestCase(-6.0, -3, -3)]
        public void Test_Sub(double a, double b, double expRes)
        {
            Assert.AreEqual(expRes, Calculator.Sub(a,b));
        }

        [TestCase(5.5, 3, 16.5)]
        [TestCase(0.0, -3, 0)]
        [TestCase(5, 0, 0)]
        [TestCase(-6.0, -3, 18)]
        public void Test_Multiply(double a, double b, double expRes)
        {
            Assert.AreEqual(expRes, Calculator.Multiply(a, b));
        }

        [TestCase(5.5, 3, 1.83333333333)]
        [TestCase(0.0, -3, 0)]
        [TestCase(-6.0, -3, 2)]
        public void Test_Divide(double a, double b, double expRes)
        {
            Assert.AreEqual(expRes, Calculator.Divide(a, b),1e-10);
        }

        [Test]
        public void Test_DivideByZero()
        {
            Assert.Throws<DivideByZeroException>(() => Calculator.Divide(6.0, 0));
        }

        [TestCase(1000d, ExpectedResult = 3)]
        [TestCase(1, ExpectedResult = 0)]
        public double Test_Logarithm10_WithSolution(double a)
        {
            return Calculator.Logarithm10(a);
        }

        [Test]
        public void Test_Logarithm10_Undefined([Values(0,-5)] double a)
        {
            Assert.Throws<ArgumentException>(()=> Calculator.Logarithm10(a));
        }

        [TestCase(25d, 5)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        public void Test_SquareRoot(double a, double expRes)
        {
            Assert.AreEqual(expRes, Calculator.SquareRoot(a));
        }
        [Test]
        public void Test_SquareRoot_Undefined([Values(-25)] double a)
        {
            Assert.Throws<ArgumentException>(()=> Calculator.Logarithm10(a));
        }

        [TestCase(5.2,0, 1)]
        [TestCase(0,0,1)]
        [TestCase(-3,1,-3)]
        [TestCase(2,-2,0.25)]
        public void Test_Power(double a, double b, double expRes)
        {
            Assert.AreEqual(expRes, Calculator.Power(a,b));
        }

        [Test]
        public void TestCalculateExpression_NoBrackets()
        {
            double resultExpected = 8;
            double resultActual = Calculator.CalculateExpression("5*2-10/5");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void TestCalculateExpression_Brackets()
        {
            double resultExpected = 12;
            double resultActual = Calculator.CalculateExpression("(5*(22-10)/5.0)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void TestCalculateExpression_NegativeInCalculation()
        {
            double resultExpected = -8;
            double resultActual = Calculator.CalculateExpression("(5*(2-10)/5.0)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void TestCalculateExpression_NegativeInExpression()
        {
            double resultExpected = 8;
            double resultActual = Calculator.CalculateExpression("(5*2-(-10)/(-5.0))");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void TestCalculateExpression_OneOperand()
        {
            double resultExpected = 2;  //5*2-(-10)/lg(105-5)*sqrt(25)
            double resultActual = Calculator.CalculateExpression("lg(105-5)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void TestCalculateExpression_AllInclude()
        {
            double resultExpected = -8.65942655433;  //   
            double resultActual = Calculator.CalculateExpression("lg(8)^(-5)+85/19.8*lg(lg(sqrt(9)))*sqrt(56)");
            Assert.AreEqual(resultExpected, resultActual,1e-10);
        }

    }
}
