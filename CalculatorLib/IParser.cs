using System.Collections.Generic;

namespace CalculatorLib
{
    public interface IParser
    {
        List<string> Parse(string expr);
    }
}