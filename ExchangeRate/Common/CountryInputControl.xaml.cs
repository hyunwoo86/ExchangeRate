using System.Windows;
using System.Windows.Controls;

namespace ExchangeRate.Common
{
    /// <summary>
    /// CountryInputControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CountryInputControl : UserControl
    {
        public CountryInputControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CountryNameProperty =
            DependencyProperty.Register(nameof(CountryName), typeof(string), typeof(CountryInputControl), new PropertyMetadata(""));

        public static readonly DependencyProperty CountryCostProperty =
            DependencyProperty.Register(nameof(CountryCost), typeof(string), typeof(CountryInputControl), new PropertyMetadata(""));

        public string CountryName
        {
            get => (string)GetValue(CountryNameProperty);
            set => SetValue(CountryNameProperty, value);
        }

        public string CountryCost
        {
            get => (string)GetValue(CountryCostProperty);
            set => SetValue(CountryCostProperty, value);
        }
    }
}
