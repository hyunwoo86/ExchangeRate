using ExchangeRate.Services;
using ExchangeRate.Views;
using System.Windows;

namespace ExchangeRate
{
    public partial class MainWindow : Window
    {
        public MainWindow(Mainpage mainpage)
        {
            InitializeComponent();
            MainFrame.Navigate(mainpage);
        }
    }
}