
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using TalkCalc.Recognizer;

namespace TalkCalc
{
    public abstract class RecognizerCommand : DependencyObject, ICommand
    {
        public static readonly DependencyProperty EngineProperty =
            DependencyProperty.Register("Engine", typeof(IRecognizerEngine),
            typeof(RecognizerCommand), new PropertyMetadata(null));


        public event EventHandler CanExecuteChanged;

        public IRecognizerEngine Engine
        {
            get { return (IRecognizerEngine)GetValue(EngineProperty); }
            set { SetValue(EngineProperty, value); }
        }


        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == EngineProperty)
            {
                if (e.OldValue != null)
                    ((IRecognizerEngine)e.OldValue).PropertyChanged -= enginePropertyChanged;
                if (e.NewValue != null)
                    ((IRecognizerEngine)e.NewValue).PropertyChanged += enginePropertyChanged;
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);

            CommandManager.InvalidateRequerySuggested();
        }


        private void enginePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.EndsWith("IsRecognizing")) OnCanExecuteChanged();
        }
    }
}