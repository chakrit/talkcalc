
using System.Windows;
using System.Diagnostics;

namespace TalkCalc.Recognizer
{
    public partial class Recognizer
    {
        public static readonly DependencyPropertyKey IsRecognizingPropertyKey =
            DependencyProperty.RegisterReadOnly("IsRecognizing", typeof(bool),
            typeof(Recognizer), new FrameworkPropertyMetadata(false));

        public static readonly DependencyPropertyKey HasResultPropertyKey =
            DependencyProperty.RegisterReadOnly("HasResult", typeof(bool),
            typeof(Recognizer), new FrameworkPropertyMetadata(false));

        public static readonly DependencyPropertyKey ResultPropertyKey =
            DependencyProperty.RegisterReadOnly("Result", typeof(string),
            typeof(Recognizer), new FrameworkPropertyMetadata(null));


        public static readonly DependencyProperty IsRecognizingProperty = IsRecognizingPropertyKey.DependencyProperty;
        public static readonly DependencyProperty HasResultProperty = HasResultPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ResultProperty = ResultPropertyKey.DependencyProperty;


        public bool IsRecognizing
        {
            get { return (bool)GetValue(IsRecognizingProperty); }
            protected set { SetValue(IsRecognizingPropertyKey, value); }
        }

        public bool HasResult
        {
            get { return (bool)GetValue(HasResultProperty); }
            protected set { SetValue(HasResultPropertyKey, value); }
        }

        public string Result
        {
            get { return (string)GetValue(ResultProperty); }
            protected set
            {
                Debug.WriteLine("RESULT HAS BEEN SET - RESULT HAS BEEN SET");
                if (value != null && !HasResult)
                    HasResult = true;
                else if (value == null && HasResult)
                    HasResult = false;

                SetValue(ResultPropertyKey, value);
            }
        }
    }
}
