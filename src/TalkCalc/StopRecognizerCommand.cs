
namespace TalkCalc
{
    public sealed class StopRecognizerCommand : RecognizerCommand
    {
        public override bool CanExecute(object parameter)
        {
            return Engine.IsRecognizing;
        }

        public override void Execute(object parameter)
        {
            Engine.Stop();
        }
    }
}