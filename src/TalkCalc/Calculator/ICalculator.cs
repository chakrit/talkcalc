
using System.ComponentModel;

namespace TalkCalc.Calculator
{
    public interface ICalculator : INotifyPropertyChanged
    {
        string Expression { get; set; }
        float Result { get; }

        float Calculate();
    }
}
