using System.Windows.Controls;
using ExchangeRate.ViewModels;
namespace ExchangeRate.Views
{
    /// <summary>
    /// Mainpage.xaml에 대한 상호 작용 논리              
    /// </summary>
    public partial class Mainpage : Page
    {
        public Mainpage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
        }

        //private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    List<string> parameter = new List<string>();
        //    parameter.Add("test ing");
        //    WeakReferenceMessenger.Default.Send(new NavigationMessage(typeof(VersionInfoPage), parameter));
        //}
    }
}
