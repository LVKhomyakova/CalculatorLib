using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using CalculatorLib;

namespace CalculatorLibTests
{
    class ExpressionCalculatorTests
    {
        [Test]
        public void Test_CalculateExpression_NoBrackets_OneOperation()
        {
            var mockP = new Mock<IParser>();
            mockP.Setup(p => p.Parse("5*2")).Returns(new List<string>() { "5","*","2"});
            IParser mockParser = mockP.Object;

            var mockRPR = new Mock<IReversePolishRecord>();
            mockRPR.Setup(p => p.Code(new List<string>() { "5", "*", "2" })).Returns(new List<string>() { "5","2","*"});
            mockRPR.Setup(p => p.Decode(new List<string>() { "5", "2", "*" })).Returns(10);
            IReversePolishRecord mockReversePolishRecor = mockRPR.Object;

            double resultExpected = 10;
            double resultActual = new ExpressionCalculator(mockParser, mockReversePolishRecor).Calculate("5*2");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void Test_CalculateExpression_NoBrackets_SomeOperations()
        {
            double resultExpected = 8;
            double resultActual = new ExpressionCalculator().Calculate("5*2-10/5");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void Test_CalculateExpression_Brackets_OneOperation()
        {
            double resultExpected = -30;
            double resultActual = new ExpressionCalculator().Calculate("(20-50.0)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void Test_CalculateExpression_Brackets_SomeOperation()
        {
            double resultExpected = 12;
            double resultActual = new ExpressionCalculator().Calculate("(5*(22-10)/5.0)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void Test_CalculateExpression_NegativeInCalculation()
        {
            double resultExpected = -8;
            double resultActual = new ExpressionCalculator().Calculate("(5*(2-10)/5.0)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void Test_CalculateExpression_NegativeInExpression()
        {
            double resultExpected = 8;
            double resultActual = new ExpressionCalculator().Calculate("(5*2-(-10)/(-5.0))");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void Test_CalculateExpression_OperationWithOneOperand()
        {
            double resultExpected = 2; 
            double resultActual = new ExpressionCalculator().Calculate("lg(105-5)");
            Assert.AreEqual(resultExpected, resultActual);
        }

        [Test]
        public void Test_CalculateExpression_AllInclude()
        {
            double resultExpected = -8.65942655433;  
            double resultActual = new ExpressionCalculator().Calculate("lg(8)^(-5)+85/19.8*lg(lg(sqrt(9)))*sqrt(56)");
            Assert.AreEqual(resultExpected, resultActual, 1e-10);
        }
    }
}
