
using System.Windows;
using System.ComponentModel;

namespace TalkCalc.Calculator
{
    public partial class Calculator : DependencyObject, ICalculator
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public float Calculate()
        {
            if (string.IsNullOrEmpty(Expression))
                return Result = 999F;

            return Result = 999F;
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.Property.Name));
        }
    }
}
