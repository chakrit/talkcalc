
using System.Speech.Recognition;

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
            var text = e.Result.Text;

            if (string.IsNullOrEmpty(Result))
                Result = text;
            else
                Result += text;
        }


        protected override string StopCore()
        {
            _engine.RecognizeAsyncStop();
            return Result;
        }
    }
}