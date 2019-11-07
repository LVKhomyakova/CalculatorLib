using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class ExpressionCalculator
    {
        IParser Parser { get; set; } = new ExpressionParser();
        IReversePolishRecord ReversePolishRecord { get; set; } = new ReversePolishRecord();

        public double Calculate(string expr)
        {
            Stack CalculateStack = new Stack();
            List<string> parsedExpression = Parser.Parse(expr); 
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
                            currentValue = CalculateStack.Pop().ToString();
                        }
                        expressionPart.Remove(")");
                        expressionPart.Reverse();
                        CalculateStack.Push(ReversePolishRecord.Decode(ReversePolishRecord.Code(expressionPart)));
                        expressionPart.Clear();
                    }
                    else
                    {
                        CalculateStack.Push(item);
                    }
                }
            }
            else  //если выражение простое без скобок и сложных операций
            {
                CalculateStack.Push(ReversePolishRecord.Decode(ReversePolishRecord.Code(parsedExpression)));
            }
            if (CalculateStack.Count > 1) //если в начале была префиксная операция, необходимо доп. решение
            {
                while (CalculateStack.Count != 0)
                    expressionPart.Add(CalculateStack.Pop().ToString());
                expressionPart.Reverse();
                CalculateStack.Push(ReversePolishRecord.Decode(ReversePolishRecord.Code(expressionPart)));
                expressionPart.Clear();
            }
            return double.Parse(CalculateStack.Pop().ToString());
        }
    
    }
}
