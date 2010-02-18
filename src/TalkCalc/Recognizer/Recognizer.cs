
using System.ComponentModel;
using System.Windows;

using Expression = System.Linq.Expressions.Expression;

namespace TalkCalc.Recognizer
{
    public partial class Recognizer : DependencyObject, IRecognizerEngine
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
        protected virtual Expression StopCore()
        {
            // test expression
            return Expression.Add(Expression.Constant(4),
                Expression.Multiply(Expression.Constant(5), Expression.Constant(6)));
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.Property.Name));
        }
    }

}
