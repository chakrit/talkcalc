﻿
using System.ComponentModel;
using System.Windows;

using Expression = System.Linq.Expressions.Expression;

namespace TalkCalc.Recognizer
{
    public interface IRecognizerEngine : INotifyPropertyChanged
    {
        bool IsRecognizing { get; }

        bool HasResult { get; }
        string Result { get; }

        void Start();
        void Stop();
    }
}
