using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calc.Model;

namespace Calc.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        static List<Button> ButtonList;

        public MainWindowView()
        {
            InitializeComponent();
            ButtonList = new List<Button> { BackSpaceButton, SighnChangerButton, RootFuncButton, SevenButton,
                    EightButton, NineButton, DevideFuncButton, PercenFuncButton, FourButton, FiveButton, SixButton,
                    MultiplyFuncButton, HyperbolaFuncButton, OneButton, TwoButton, ThreeButton, MinusFuncButton, ZeroButton,
                    DecimalButton, PlusFuncButton, ResultButton, StyleChanger, OpenBracket, CloseBracket, ClearButton, ClearEntryButton };
        }

        private void StyleChanger_Click(object sender, RoutedEventArgs e)
        {                                                                                                        
            if (Grid.Style == (Style)Resources["GridMainStyle"])
            {
                Grid.Style = (Style)Resources["GridSecondaryStyle"];
                TextBoxBackground.Style = (Style)Resources["TextBoxBackgroundSecondaryStyle"];
                foreach(Button b in ButtonList)
                    b.Style = (Style)Resources["ButtonSecondaryStyle"];
            }
            else if(Grid.Style == (Style)Resources["GridSecondaryStyle"])
            {
                Grid.Style = (Style)Resources["GridMainStyle"];
                TextBoxBackground.Style = (Style)Resources["TextBoxBackgroundMainStyle"];
                foreach (Button b in ButtonList)
                    b.Style = (Style)Resources["ButtonMainStyle"];
            }
        }
    }
}
