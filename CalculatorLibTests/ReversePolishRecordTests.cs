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
    class ReversePolishRecordTests
    {
        public static object[] codeCases = new object[] 
        {
            new object[]{new List<string>()  { "4", "+", "5" }              //4 + 5
                            ,new List<string>()  { "4", "5", "+" } },       //4 5 +

            new object[]{new List<string>()  { "lg", "100","^","-2" }           //lg100 ^ -2
                            ,new List<string>()  { "100", "lg", "-2", "^" } },  //100 lg -2 ^

            new object[]{new List<string>()  { "5", "-", "-8","/","2"}          //5 - -8 / 2
                            ,new List<string>()  { "5", "-8", "2","/","-"} },   //5 -8 2 / -

            new object[]{new List<string>()  { "5", "*", "-8","/","5.0"}        //5 * -8 / 5.0
                            ,new List<string>()  { "5", "-8", "*","5.0","/"} }  //5 -8 * 5.0 /

        };

        [TestCaseSource("codeCases")]
        public void Test_Code(List<string> input, List<string> expected)
        {
            CollectionAssert.AreEqual(expected, new ReversePolishRecord().Code(input));
        }

        public static object[] decodeCases = new object[] 
{
            new object[]{new List<string>()  { "4", "5", "+" }, 9},             // 4+5=9
            new object[]{new List<string>()  { "100", "lg" }, 2 },              //lg100=2
            new object[]{new List<string>()  { "100", "lg", "-2", "^" }, 0.25 },//lg100^2=4
            new object[]{new List<string>() { "5", "-8", "2", "/", "-" }, 9 }   //5-(-8)/2=9
        };

        [TestCaseSource("decodeCases")]
        public void Test_Decode(List<string> input, double expected)
        {
            Assert.AreEqual(expected, new ReversePolishRecord().Decode(input));
        }
    }
}
