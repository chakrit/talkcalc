
using System.Windows;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System;

namespace TalkCalc.Calculator
{
    public partial class Calculator : DependencyObject, ICalculator
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public float Calculate()
        {
            if (string.IsNullOrEmpty(Expression))
                return Result = 999F;

            return Result = 777F;
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.Property.Name));
        }
    }
}
