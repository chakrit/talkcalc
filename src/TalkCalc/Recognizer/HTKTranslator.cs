
using System;
using System.Diagnostics;
using System.Text;

namespace TalkCalc.Recognizer
{
    // translates HTK dict words into calculable arithmetic expression
    public class HTKTranslator
    {
        private bool _ready;


        public void Reset() { _ready = false; }

        public string Translate(string htkResultLine)
        {
            Debug.WriteLine("HTK> " + htkResultLine);

            if (string.IsNullOrEmpty(htkResultLine))
                return string.Empty;

            // wait until htk stops level measuring
            if (!_ready)
            {
                if (htkResultLine.Contains(@"Level measurement complete"))
                    _ready = true;

                return string.Empty;
            }

            if (!htkResultLine.StartsWith("START-SIL"))
                // assume the line passed in ss a useless info line, nothing to translate
                return string.Empty;


            return innerTranslate(htkResultLine);
        }


        private string innerTranslate(string htkResultLine)
        {
            var tokens = htkResultLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new StringBuilder();

            var isEnd = false;
            var curNum = 0;
            var lastDigit = 0;

            // state transitions
            Action<int> flushNum = n =>
            {
                result.Append(" " + n.ToString() + " ");
                curNum = lastDigit = 0;
            };

            Action acceptZero = () => flushNum(0);
            Action<int> acceptDigit = d => lastDigit = d;

            Action<int> acceptModifier = m =>
            {
                curNum += Math.Max(lastDigit, 1) * m;
                lastDigit = 0;
            };

            Action<string> acceptOp = o =>
            {
                flushNum(curNum += lastDigit);
                result.Append(o);
            };

            Action acceptEqual = () =>
            {
                flushNum(curNum += lastDigit);
                isEnd = true;
            };

            // run the tokens through this simple state machine
            foreach (var token in tokens)
            {
                switch (token)
                {
                    case "START-SIL": /* absorbed */ break;

                    case "ZERO": acceptZero(); break;

                    case "ONE":
                    case "ONE_ALT": acceptDigit(1); break;
                    case "TWO":
                    case "TWO_ALT": acceptDigit(2); break;
                    case "THREE": acceptDigit(3); break;
                    case "FOUR": acceptDigit(4); break;
                    case "FIVE": acceptDigit(5); break;
                    case "SIX": acceptDigit(6); break;
                    case "SEVEN": acceptDigit(7); break;
                    case "EIGHT": acceptDigit(8); break;
                    case "NINE": acceptDigit(9); break;

                    case "TEN": acceptModifier(10); break;
                    case "HUNDRED": acceptModifier(100); break;
                    case "THOUSAND": acceptModifier(1000); break;
                    case "TENTHOUSAND": acceptModifier(10000); break;
                    case "HUNDREDTHOUSAND": acceptModifier(100000); break;

                    case "PLUS": acceptOp("+"); break;
                    case "MINUS": acceptOp("-"); break;
                    case "MULTIPLY": acceptOp("*"); break;
                    case "DIVIDE": acceptOp("/"); break;

                    case "EQUAL":
                    case "END-SIL": acceptEqual(); break;

                    case "S":
                    default:
                        /* absorbed but really should throw exception
                         * since we shouldn't have gotten to this point in code */
                        break;
                }

                if (isEnd) break;
            }

            Debug.WriteLine("TRANSLATOR > " + result.ToString());
            return result.ToString();
        }

    }
}
