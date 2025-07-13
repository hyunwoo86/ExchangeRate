using CommunityToolkit.Mvvm.ComponentModel;

namespace ExchangeRate.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _costUSD = "123123";

        [ObservableProperty]
        private string _costEUR = "123123";

        [ObservableProperty]
        private string _costJYP = "123123";

        [ObservableProperty]
        private string _costCNY = "123123";

        public MainPageViewModel()
        {
            
        }
    }
}
