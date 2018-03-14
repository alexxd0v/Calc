using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Model
{
    class CalcModel
    {
        public static string sqrt = System.Text.RegularExpressions.Regex.Unescape("\u221a");
        private string input;
        private string result;
        private Stack<double> operands;
        private Stack<string> operators;

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                input = value;
            }
        }
        public string Result
        {
            get
            {
                if (operands.Count == 1)
                {
                    result = operands.Peek().ToString();
                    input = String.Empty;
                    operands.Clear();
                    operators.Clear();
                }
                return result;
            }
        }

        public CalcModel()
        {
            input = String.Empty;
            result = String.Empty;
            operands = new Stack<double>();
            operators = new Stack<string>();
        }

        public void Calculate()
        {
            input = '(' + input + ')';
            int position = 0;
            string symbol = String.Empty;
            string prevSymbol = String.Empty;

            do
            {
                symbol = GetSimbol(ref position);
                if (!double.TryParse(symbol, out double t) && symbol == sqrt)
                    operands.Push(2d);              
                if (!double.TryParse(symbol, out t) && prevSymbol == "(" && (symbol == "+" || symbol == "-"))
                    operands.Push(0d);                
                if (double.TryParse(symbol, out t))
                {
                    operands.Push(double.Parse(symbol));
                }
                else
                {
                    if (symbol == ")")
                    {
                        while (operators.Count > 0 && operators.Peek() != "(")
                            PopCalc();
                        operators.Pop();
                    }
                    else
                    {
                        while (CanPop(symbol))
                            PopCalc();
                        operators.Push(symbol);
                    }
                }
                prevSymbol = symbol;
            }
            while (symbol != null);
        }

        private void PopCalc()
        {
            double right = operands.Pop();
            double left = operands.Pop();
            string op = operators.Pop();
            switch (op)
            {
                case "+":
                    {
                        operands.Push(left + right);
                        break;
                    }
                case "-":
                    {
                        operands.Push(left - right);
                        break;
                    }
                case "*":
                    {
                        operands.Push(left * right);
                        break;
                    }
                case "/":
                    {
                        operands.Push(left / right);
                        break;
                    }
                case "%":
                    {
                        operands.Push((left/100)*right);
                        break;
                    }
                default:
                    {
                        if (op.Contains(sqrt))
                        {
                            if (right < 0)
                                throw new Exception("Отрицательное подкоренное выражение");
                            else operands.Push(Math.Pow(right, 1 / left));
                            break;
                        }
                        break;
                    }
            }
        }

        private int GetPriority(string op)
        {
            switch(op)
            {
                case "(": return -1;
                case "*": return 1;
                case "/": return 1;
                case "+": return 2;
                case "-": return 2;
                case "%": return 4;
                default:
                    {
                        if (op!=null && op.Contains(sqrt))
                            return 3;
                        return 0;
                    }
            }
        }

        private string GetSimbol(ref int position)
        {
            if (position == input.Length)
                return null;
            if ("1234568790".Contains(input[position]))
                return ReadDouble(ref position);
            else
                return ReadOperator(ref position);
        }

        private bool CanPop(string _operator)
        {
            if (operators.Count == 0)
                return false;
            int operator1 = GetPriority(_operator);
            int operator2 = GetPriority(operators.Peek());
            return (operator1 > 0 && operator2 > 0 && operator1 >= operator2);
        }

        private string ReadDouble(ref int position)
        {
            string number = String.Empty;
            while (position < input.Length && "1234567890.,".Contains(input[position]))
            {
                if (input[position].ToString() == ".")
                {
                    number += ",";
                    position++;
                }
                else
                {
                    number += input[position];
                    position++;
                }
            }
            return number;
        }

        private string ReadOperator(ref int position)
        {
            return input[position++].ToString();
        }
    }
}
