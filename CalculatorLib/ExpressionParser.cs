using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class ExpressionParser: IParser
    {
        public List<string> Parse(string expr)
        {
            Regex regex = new Regex(@"[0-9.]+|[-+*/^]{1}|[()]{1}|[a-z]+");
            MatchCollection mc = regex.Matches(expr);
            List<string> parsedExpression = new List<string>();
            foreach (Match item in mc)
            {
                parsedExpression.Add(item.Value);
            }
            return parsedExpression;
        }

    }
}
