using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class Calculator
    {
        public static double Sum(double a,double b)
        {
            return a + b;
        }

        public static double Sub(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            if (b != 0.0)
                return a / b;
            throw new DivideByZeroException();
        }

        public static double Logarithm10(double a)
        {
            if (a > 0.0)
                return Math.Log10(a);
            throw new ArgumentException();
        }

        public static double SquareRoot(double a)
        {
            if(a>=0)
                return Math.Sqrt(a);
            throw new ArgumentException();
        }

        public static double Power(double a, double b)
        {
            return Math.Pow(a,b);
        }
    }
}
