using CommunityToolkit.Mvvm.Messaging;
using ExchangeRate.Common.Navigations;
using ExchangeRate.Views;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeRate
{
    public partial class MainWindow : Window
    {
        public MainWindow(Mainpage mainpage)
        {
            InitializeComponent();

            RegisterNavigationMessageHandler();

            //WeakReferenceMessenger.Default.Register<NavigationMessage, string>(this, "TEST1", OnLayerPopupMessageToken1);
            //WeakReferenceMessenger.Default.Register<NavigationMessage, string>(this, "TEST2", OnLayerPopupMessageToken2);
        }

        //private void OnLayerPopupMessageToken2(object recipient, NavigationMessage message)
        //{
        //    MessageBox.Show("token2");
        //}

        //private void OnLayerPopupMessageToken1(object recipient, NavigationMessage message)
        //{
        //    MessageBox.Show("token1");
        //}

        private void RegisterNavigationMessageHandler()
        {
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this, OnNavigateToPage);
        }

        private void OnNavigateToPage(object recipient, NavigationMessage message)
        {
            var page = (Page?)Activator.CreateInstance(message.TargetPageType);

            if (page != null)
            {
                MainFrame.Navigate(page);
            }
            else
            {
                MessageBox.Show($"페이지 생성 실패: {message.TargetPageType.Name}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}