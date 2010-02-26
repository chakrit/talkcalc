
using System.Windows;
using System.Diagnostics;

namespace TalkCalc.Calculator
{
    public partial class Calculator
    {
        public static readonly DependencyPropertyKey ResultPropertyKey =
            DependencyProperty.RegisterReadOnly("Result", typeof(float),
            typeof(Calculator), new FrameworkPropertyMetadata(0F));

        public static readonly DependencyProperty ResultProperty = ResultPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ExpressionProperty =
            DependencyProperty.Register("Expression", typeof(string),
            typeof(Calculator), new FrameworkPropertyMetadata(null));


        public string Expression
        {
            get { return (string)GetValue(ExpressionProperty); }
            set
            {
                Debug.WriteLine("EXPRESSION HAS BEEN SET - EXPRESSION HAS BEEN SET");
                SetValue(ExpressionProperty, value);
                Calculate();
            }
        }

        public float Result
        {
            get { return (float)GetValue(ResultProperty); }
            protected set { SetValue(ResultPropertyKey, value); }
        }


    }
}
