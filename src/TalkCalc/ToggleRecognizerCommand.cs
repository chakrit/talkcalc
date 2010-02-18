
namespace TalkCalc
{
    public class ToggleRecognizerCommand : RecognizerCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (Engine.IsRecognizing)
                Engine.Stop();
            else
                Engine.Start();
        }
    }
}