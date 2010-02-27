
using System.Diagnostics;

namespace TalkCalc.Recognizer
{
    // translates HTK dict words into calculable arithmetic expression
    public class HTKTranslator
    {
        public string Translate(string htkResultLine)
        {
            Debug.WriteLine("HTK> " + htkResultLine);
            return "4 + 5*6 + 7";
        }
    }
}
