
using System.ComponentModel;
using System.Windows;

using Expression = System.Linq.Expressions.Expression;

namespace TalkCalc.Recognizer
{
    public partial class Recognizer : DependencyObject, IRecognizer
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public Recognizer() { }


        public void Start()
        {
            StartCore();

            IsRecognizing = true;
        }

        public void Stop()
        {
            var result = StopCore();

            IsRecognizing = false;
            HasResult = true;
            Result = result;
        }


        protected virtual void StartCore() { }
        protected virtual string StopCore()
        {
            // test expression
            return "4 + 5*6";
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.Property.Name));
        }
    }

}
