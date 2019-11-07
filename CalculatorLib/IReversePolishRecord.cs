using System.Collections.Generic;

namespace CalculatorLib
{
    public interface IReversePolishRecord
    {
        List<string> Code(List<string> expr);
        double Decode(List<string> ReversePolishRecord);
    }
}