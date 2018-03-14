using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Calc.View;
using Calc.ViewModel;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindowView win = new MainWindowView()
            {
                DataContext = new MainWindowViewModel()
            };
            win.Show();
        }
    }
}
