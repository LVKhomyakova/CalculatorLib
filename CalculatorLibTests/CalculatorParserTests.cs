using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CalculatorLib;

namespace CalculatorLibTests
{
    [TestFixture]
    class CalculatorParserTests
    {
        [Test]
        public void TestParseExpression()
        {
            List<string> resultExpected = new List<string> { "(", "5", "*", "(", "2", "-", "10", ")", "/", "5.0", ")" };
            List<string> resultActual = CalculatorParser.ParseExpression("(5*(2-10)/5.0)");
            for (int i = 0; i < resultExpected.Count; i++)
            {
                Assert.AreEqual(resultExpected[i], resultActual[i]);
            }
        }

    }
}
