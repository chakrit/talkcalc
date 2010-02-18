
namespace TalkCalc
{
    public sealed class StartRecognizerCommand : RecognizerCommand
    {
        public override bool CanExecute(object parameter)
        {
            return !Engine.IsRecognizing;
        }

        public override void Execute(object parameter)
        {
            Engine.Start();
        }
    }
}