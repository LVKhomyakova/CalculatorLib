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
        static Stack ParseStack { get; set; } = new Stack();
        static List<string> ReversePolishRecord { get; set; } = new List<string>();

        static Dictionary<string, int> Operations { get; set; } 
            = new Dictionary<string, int> {
                {"+", 1 },
                {"-", 1 },
                {"*", 2 },
                {"/", 2 },
                {"sqrt", 3 },
                {"^", 3 },
                {"lg", 3 } //lg основание 10
            };
        
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

        static double DoOperation(double leftOp, double rightOp, string op)
        {
            switch (op)
            {
                case "+":
                    return Sum(leftOp, rightOp);
                case "-":
                    return Sub(leftOp, rightOp);
                case "*":
                    return Multiply(leftOp, rightOp);
                case "/":
                    return Divide(leftOp, rightOp);
                case "lg":
                    return Logarithm10(rightOp);
                case "sqrt":
                    return SquareRoot(rightOp);
                case "^":
                    return Power(leftOp, rightOp);
                default:
                    return double.NaN;
            }
        }
        static double DecodeReversePolishRecord()
        {
            Stack stackValues = new Stack();
            double leftOperand = 0.0;
            double rightOperand = double.NaN;
            string operation = string.Empty;
            foreach (string item in ReversePolishRecord)
            {
                if (char.IsDigit(item.First()) || char.IsDigit(item.Last()))
                {
                    stackValues.Push(item);
                }
                else
                {
                    operation = item;
                    rightOperand = double.Parse(stackValues.Pop().ToString());
                    if (operation.CompareTo("sqrt") != 0 && operation.CompareTo("lg") != 0)
                    {
                        if (stackValues.Count != 0)
                            leftOperand = double.Parse(stackValues.Pop().ToString());
                    }
                    stackValues.Push(DoOperation(leftOperand, rightOperand, operation));
                }
            }
            ReversePolishRecord.Clear();
            return double.Parse(stackValues.Pop().ToString());
        }
        static void CodeReversePolishRecord(List<string> expr)
        {
            Stack operands = new Stack();

            foreach (var item in expr)
            {
                if (char.IsDigit(item.First()) || char.IsDigit(item.Last()))
                {
                    ReversePolishRecord.Add(item);
                }
                else
                {
                    if (operands.Count == 0)
                    {
                        operands.Push(item);
                    }
                    else if (Operations[item]>Operations[operands.Peek().ToString()])
                    {
                        operands.Push(item);
                    }
                    else
                    {
                        while (operands.Count != 0)
                        {
                            if (Operations[operands.Peek().ToString()] >= Operations[item])
                                ReversePolishRecord.Add(operands.Pop().ToString());
                            else
                                break;
                        }
                        operands.Push(item);
                    }
                }
            }
            while (operands.Count != 0)
            {
                ReversePolishRecord.Add(operands.Pop().ToString());
            }
        }
        public static double CalculateExpression(string expr)
        {
            List<string> parsedExpression = CalculatorParser.ParseExpression(expr);
            List<string> expressionPart = new List<string>();
            if (parsedExpression.Contains("(")) //если выражение сложное (со скобками или сложными операциями)
            {
                foreach (var item in parsedExpression)
                {
                    string currentValue = item;
                    if (currentValue.CompareTo(")") == 0)
                    {
                        while (currentValue.CompareTo("(") != 0)
                        {
                            expressionPart.Add(currentValue);
                            currentValue = ParseStack.Pop().ToString();
                        }
                        expressionPart.Remove(")");
                        expressionPart.Reverse();
                        CodeReversePolishRecord(expressionPart);
                        expressionPart.Clear();
                        ParseStack.Push(DecodeReversePolishRecord());
                    }
                    else
                    {
                        ParseStack.Push(item);
                    }
                }
            }
            else  //если выражение простое без скобок и сложных операций
            {
                CodeReversePolishRecord(parsedExpression);
                ParseStack.Push(DecodeReversePolishRecord());
            }
            if (ParseStack.Count > 1) //если в начале была сложная операция, необходимо доп. решение
            {
                while(ParseStack.Count!=0)
                    expressionPart.Add(ParseStack.Pop().ToString());
                expressionPart.Reverse();
                CodeReversePolishRecord(expressionPart);
                expressionPart.Clear();
                ParseStack.Push(DecodeReversePolishRecord());
            }
            return double.Parse(ParseStack.Pop().ToString());
        }
    }
}
