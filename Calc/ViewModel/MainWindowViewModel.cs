using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calc.Model;
using System.Windows.Input;

namespace Calc.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private CalcModel calc;
        private int openBracketCount;
        private int closeBracketCount;

        public MainWindowViewModel()
        {
            calc = new CalcModel();
            result = String.Empty;
            openBracketCount = 0;
            closeBracketCount = 0;
            ButtonCommand = new Command(s => InputMethod(s as string));
        }

        string result;
        public string Input
        {
            get
            {
                return calc.Input;
            }
            set
            {
                calc.Input = value;
                OnPropertyChanged("Input");
            }
        }

        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        public ICommand ButtonCommand { get; set; }

        private void InputMethod(string s)
        {
            switch (s)
            {
                case "Clear":
                    {
                        Input = String.Empty;
                        Result = String.Empty;
                        closeBracketCount = 0;
                        openBracketCount = 0;
                        break;
                    }
                case "ClearEntry":
                    {
                        Result = String.Empty;
                        break;
                    }
                case "BackSpace":
                    {
                        if (Result.Length > 0 && char.IsLetter(Result[0]))
                            Result = String.Empty;
                        if(Result.Length>0)
                            Result = Result.Remove(Result.Length - 1);
                        break;
                    }
                case "+/-":
                    {
                        if (Result.Length > 0 && Result[0] != '-') 
                        {
                            Result = Result.Insert(0, "-");
                            break;
                        }
                        else if (Result.Length > 0 && Result[0] == '-')
                        {
                            Result = Result.Remove(0,1);
                            break;
                        }
                        break;
                    }
                case "=":
                    {
                        if (Input.Length > 0)
                        {
                            if (Result.Length > 0)
                            {
                                Input += Result;
                                if (Result.Length > 0 && openBracketCount > closeBracketCount)
                                {
                                    for (int i = 0; i < Math.Abs(closeBracketCount - openBracketCount); i++)
                                    {
                                        Input += ")";
                                    }
                                }
                                try
                                {
                                    calc.Calculate();
                                    Result = calc.Result;
                                    openBracketCount = 0; closeBracketCount = 0;
                                }
                                catch (Exception e)
                                {
                                    Input = e.Message;
                                }
                                
                            }
                            else break;
                            
                        }
                        break;
                    }
                case "1/x":
                    {
                        if (Result.Length > 0 && Result[0] != '-')
                        {
                            if (Input.Length > 0)
                            {
                                string temp = Input;
                                Input = "1/" + Result;
                                calc.Calculate();
                                Input = temp + calc.Result;
                                if (Result.Length > 0 && openBracketCount > closeBracketCount)
                                {
                                    for (int i = 0; i < Math.Abs(closeBracketCount - openBracketCount); i++)
                                    {
                                        Input += ")";
                                    }
                                }
                                calc.Calculate();
                                Result = calc.Result;
                                openBracketCount = 0;
                                closeBracketCount = 0;
                                break;
                            }
                            else if (Input.Length == 0)
                            {
                                Input += "1/" + Result;
                                calc.Calculate();
                                Result = calc.Result;
                                break;
                            }
                        }
                        else if (Result.Length > 0 && Result[0] == '-')
                        {
                            Input += "1/(" + Result + ")";
                            if (Result.Length > 0 && openBracketCount > closeBracketCount)
                            {
                                for (int i = 0; i < Math.Abs(closeBracketCount - openBracketCount); i++)
                                {
                                    Input += ")";
                                }
                            }
                            calc.Calculate();
                            Result = calc.Result;
                            openBracketCount = 0;
                            closeBracketCount = 0;
                            break;
                        }
                        break;
                    }
                case "%":
                    {
                        if (Result.Length > 0)
                        {
                            Input += Result + s;
                            Result = String.Empty;
                        }
                        else if (Input.Length > 0 && Result.Length == 0 && (Input[Input.Length - 1] == '-' || Input[Input.Length - 1] == '*' || Input[Input.Length - 1] == '/' || Input[Input.Length - 1] == '+'))
                        {
                            Input = Input.Remove(Input.Length - 1);
                            Input += s;
                        }
                        break;
                    }
                case ",":
                    {
                        if (Result.Length == 0)
                        {
                            Result = "0" + s + Result;
                            break;
                        }
                        else if (Result.Contains(s))
                            break;
                        else
                        {
                            Result += s;
                            break;
                        }
                    }
                case "+":
                    {
                        if (Result.Length > 0)
                        {
                            Input += Result + s;
                            Result = String.Empty;
                        }
                        else if (Input.Length > 0 && Result.Length == 0 && (Input[Input.Length-1] == '-'|| Input[Input.Length - 1] == '*' || Input[Input.Length - 1] == '/' || Input[Input.Length - 1] == '%'))
                        {
                            Input = Input.Remove(Input.Length-1);
                            Input += s;
                        }
                        break;
                    }
                case "-":
                    {
                        if (Result.Length > 0)
                        {
                            Input += Result + s;
                            Result = String.Empty;
                        }
                        else if (Input.Length > 0 && Result.Length == 0 && (Input[Input.Length - 1] == '+' || Input[Input.Length - 1] == '*' || Input[Input.Length - 1] == '/' || Input[Input.Length - 1] == '%'))
                        {
                            Input = Input.Remove(Input.Length - 1);
                            Input += s;
                        }

                        break;
                    }
                case "/":
                    {
                        if (Result.Length > 0)
                        {
                            Input += Result + s;
                            Result = String.Empty;
                        }
                        else if (Input.Length > 0 && Result.Length == 0 && (Input[Input.Length - 1] == '-' || Input[Input.Length - 1] == '*' || Input[Input.Length - 1] == '+' || Input[Input.Length - 1] == '%'))
                        {
                            Input = Input.Remove(Input.Length - 1);
                            Input += s;
                        }

                        break;
                    }
                case "*":
                    {
                        if (Result.Length > 0)
                        {
                            Input += Result + s;
                            Result = String.Empty;
                        }
                        else if (Input.Length > 0 && Result.Length == 0 && (Input[Input.Length - 1] == '-' || Input[Input.Length - 1] == '/' || Input[Input.Length - 1] == '/' || Input[Input.Length - 1] == '%'))
                        {
                            Input = Input.Remove(Input.Length - 1);
                            Input += s;
                        }

                        break;
                    }
                case "(":
                    {
                        openBracketCount++;
                        Input += s;
                        break;
                    }
                case ")":
                    {
                        if (Result.Length > 0 && openBracketCount > closeBracketCount)
                        {
                            Input += Result;
                            for (int i = 0; i < Math.Abs(closeBracketCount - openBracketCount); i++)
                            {
                                Input += ")";
                            }
                            try
                            {
                                calc.Calculate();
                                Result = calc.Result;
                                openBracketCount = 0;
                                closeBracketCount = 0;
                            }
                            catch (Exception e)
                            {
                                Input = e.Message;
                            }
                        }
                        else if (Result.Length > 0 && openBracketCount == closeBracketCount)
                        {
                            try
                            {
                                calc.Calculate();
                                Result = calc.Result;
                                openBracketCount = 0;
                                closeBracketCount = 0;
                            }
                            catch (Exception e)
                            {
                                Input = e.Message;
                            }
                        }
                        break;
                    }
                default:
                    {
                        if (Result.Length > 0 & s == CalcModel.sqrt)
                        {
                            try
                            {
                                Input = CalcModel.sqrt + "(" + Result + ")";
                                calc.Calculate();
                                Result = calc.Result;
                                openBracketCount = 0;
                                closeBracketCount = 0;
                            }
                            catch (Exception e)
                            {
                                Input = e.Message;
                            }
                            break;
                        }
                        else if (Result.Length == 0 & s == CalcModel.sqrt)
                        {
                            Result = CalcModel.sqrt + "(" + Result;
                            openBracketCount++;
                            break;
                        }
                        else
                        {
                            Result += s;
                            break;
                        }                    
                    }
            }
        }
    }
}
