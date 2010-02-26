
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

using StrStack = System.Collections.Generic.Stack<string>;
using FloatStack = System.Collections.Generic.Stack<float>;

namespace TalkCalc.Calculator
{
    public partial class Calculator : DependencyObject, ICalculator
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public float Calculate()
        {
            var expr = Expression;
            if (string.IsNullOrEmpty(expr))
                return 0F;

            // define some calculation parameters
            // division comes before multiplication to make it easy to do fractions
            var precedences = new[] { "+", "-", "/", "*" };
            Func<string, int> precendenceOf = s => Array.IndexOf(precedences, s);

            // split the expression string into tokens
            // pad operators with some spaces and so it's easy to tokenize
            var tokens = new StringBuilder(expr)
                .Replace("+", " + ")
                .Replace("-", " - ")
                .Replace("*", " * ")
                .Replace("/", " / ")
                .ToString()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // calculate the result
            var ops = new StrStack();
            var nums = new FloatStack();

            var currentOps = (string)null;

            // a modified version of:
            // http://scriptasylum.com/tutorials/infix_postfix/algorithms/infix-postfix/index.htm
            // however, we don't want the infix expression result,
            // we just need the result of the calculation calculated right away
            foreach (var token in tokens)
            {
                if (char.IsNumber(token[0]))
                    nums.Push(float.Parse(token));
                else
                {
                    if (currentOps != null && precendenceOf(currentOps) > precendenceOf(token))
                    {
                        var result = flatten(nums, ops);
                        nums.Push(result);
                    }

                    ops.Push(currentOps = token);
                }
            }

            // calculate all remaining values
            return flatten(nums, ops);
        }

        // calculates the current state and return the result
        private float flatten(FloatStack nums, StrStack ops)
        {
            var result = 0F;

            while (ops.Count > 0)
            {
                var op = ops.Pop();

                var opr1 = nums.Pop();
                var opr2 = nums.Pop();

                switch (op)
                {
                    case "+": result = opr1 + opr2; break;
                    case "-": result = opr1 - opr2; break;
                    case "*": result = opr1 * opr2; break;
                    case "/": result = opr1 / opr2; break;
                }

                nums.Push(result);
            }

            return nums.Pop();
        }


        protected virtual void OnExpressionChanged(DependencyPropertyChangedEventArgs e)
        {
            Result = Calculate();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.Property.Name));
        }


    }
}
