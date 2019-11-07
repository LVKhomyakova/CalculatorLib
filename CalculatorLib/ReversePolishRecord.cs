using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class ReversePolishRecord : IReversePolishRecord
    {
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

        double DoOperation(double leftOp, double rightOp, string op)
        {
            switch (op)
            {
                case "+":
                    return Calculator.Sum(leftOp, rightOp);
                case "-":
                    return Calculator.Sub(leftOp, rightOp);
                case "*":
                    return Calculator.Multiply(leftOp, rightOp);
                case "/":
                    return Calculator.Divide(leftOp, rightOp);
                case "lg":
                    return Calculator.Logarithm10(rightOp);
                case "sqrt":
                    return Calculator.SquareRoot(rightOp);
                case "^":
                    return Calculator.Power(leftOp, rightOp);
                default:
                    return double.NaN;
            }
        }

        public List<string> Code(List<string> expr)
        {
            List<string> ReversePolishRecord = new List<string>();
            Stack operands = new Stack();

            foreach (var item in expr)
            {
                if (char.IsDigit(item.First()) || char.IsDigit(item.Last()))
                {
                    ReversePolishRecord.Add(item);
                }
                else
                {
                    if (operands.Count==0 || Operations[item] > Operations[operands.Peek().ToString()])
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
            return ReversePolishRecord;
        }

        public double Decode(List<string> ReversePolishRecord)
        {
            Stack stackValues = new Stack();
            foreach (string item in ReversePolishRecord)
            {
                double leftOperand = 0.0;
                double rightOperand = double.NaN;
                string operation = string.Empty;
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
            return double.Parse(stackValues.Pop().ToString());
        }

    }
}
