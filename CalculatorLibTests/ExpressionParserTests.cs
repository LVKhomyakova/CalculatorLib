using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CalculatorLib;

namespace CalculatorLibTests
{
    [TestFixture,Category("Unit Tests")]
    class ExpressionParserTest
    {
        [Test]
        public void Test_ParseExpression()
        {
            List<string> resultExpected = new List<string> { "(", "5", "*", "(", "2", "-", "10", ")", "/", "5.0", ")" };
            List<string> resultActual = new ExpressionParser().Parse("(5*(2-10)/5.0)");
            for (int i = 0; i < resultExpected.Count; i++)
            {
                Assert.AreEqual(resultExpected[i], resultActual[i]);
            }
        }

        [Test]
        public void Test_ParseExpression_BeginWithNegativeNumber()
        {
            List<string> resultExpected = new List<string> { "(","-","5",")"};
            List<string> resultActual = new ExpressionParser().Parse("(-5)");
            for (int i = 0; i < resultExpected.Count; i++)
            {
                Assert.AreEqual(resultExpected[i], resultActual[i]);
            }
        }
        [Test]
        public void Test_ParseExpression_WithNegativeNumber()
        {
            List<string> resultExpected = new List<string> {"10","-","(", "-","5",")" };
            List<string> resultActual = new ExpressionParser().Parse("10-(-5)");
            for (int i = 0; i < resultExpected.Count; i++)
            {
                Assert.AreEqual(resultExpected[i], resultActual[i]);
            }
        }
        [Test]
        public void Test_ParseExpression_DoubleNegativeNumber()
        {
            List<string> resultExpected = new List<string> { "10", "+","(","-","(","-","5",")",")"};
            List<string> resultActual = new ExpressionParser().Parse("10+(-(-5))");
            for (int i = 0; i < resultExpected.Count; i++)
            {
                Assert.AreEqual(resultExpected[i], resultActual[i]);
            }
        }
    }
}
