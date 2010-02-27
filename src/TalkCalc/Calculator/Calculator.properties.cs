
using System.Windows;

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
            typeof(Calculator), new FrameworkPropertyMetadata(null, expressionChanged));


        public string Expression
        {
            get { return (string)GetValue(ExpressionProperty); }
            set { SetValue(ExpressionProperty, value); }
        }

        public float Result
        {
            get { return (float)GetValue(ResultProperty); }
            protected set { SetValue(ResultPropertyKey, value); }
        }


        private static void expressionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var calc = o as Calculator;
            if (calc == null) return;

            calc.OnExpressionChanged(e);
        }

    }
}
