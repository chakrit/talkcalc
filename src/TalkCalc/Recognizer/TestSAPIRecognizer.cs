
using System.Speech.Recognition;
using System.Linq.Expressions;

namespace TalkCalc.Recognizer
{
    public class TestSAPIRecognizer : Recognizer
    {
        private SpeechRecognitionEngine _engine;

        public TestSAPIRecognizer()
        {
            var builder = new GrammarBuilder();

            var choices = new Choices("Neung", "Saawng", "Saam", "See", "Haa", "Hohk", "Jet", "Paadt", "Gaao");
            var ops = new Choices("Buak", "Lob", "Koon", "Haan");
            builder.Append(choices);
            builder.Append(ops);

            var grammar = new Grammar(builder);

            _engine = new SpeechRecognitionEngine();
            _engine.LoadGrammar(grammar);
            _engine.SetInputToDefaultAudioDevice();

            _engine.SpeechRecognized += speechRecognized;
        }


        protected override void StartCore()
        {
            _engine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var text = Expression.Constant(e.Result.Text);

            if (Result == null)
                Result = text;
            else
            {
                var str = typeof(string);
                var method = str.GetMethod("Concat", new[] { str, str });
                Result = Expression.Call(null, method,
                    Result, Expression.Constant(e.Result.Text));
            }
        }


        protected override Expression StopCore()
        {
            _engine.RecognizeAsyncStop();
            return Result;
        }
    }
}