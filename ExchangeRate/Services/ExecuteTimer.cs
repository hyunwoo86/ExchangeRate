using CommunityToolkit.Mvvm.Messaging;
using ExchangeRate.Common;
using ExchangeRate.Common.Navigations;
using ExchangeRate.Views;
using Microsoft.Extensions.Logging;

namespace ExchangeRate.Services
{
    public class ExecuteTimer
    {
        // https://obank.kbstar.com/quics?page=C101423#loading


        CancellationToken _cancellationToken = new CancellationToken();
        MainWindow _mainWindow;
        LoadHomePage _loadHomePage;
        GetExchangeRate _getExchangeRate;
        Dictionary<string, double> _countryExchangeCost = new Dictionary<string, double>();
        private readonly ILogger<ExecuteTimer> _logger;
        bool _isRunning = false;
        public ExecuteTimer(MainWindow mainWindow, LoadHomePage loadHomePage, GetExchangeRate getExchangeRate, ILogger<ExecuteTimer> logger)
        {
            _mainWindow = mainWindow;
            _logger = logger;
            _loadHomePage = loadHomePage;
            _getExchangeRate = getExchangeRate;
            _isRunning = true;
        }

        public void Execute()
        {
            while (_isRunning)
            {
                Thread.Sleep(5000);

                _logger.LogInfoWithCaller("ExecuteTimer 시작됨");

                if (_loadHomePage.Load())
                {
                    _getExchangeRate.LoadHtml(_loadHomePage.FsullHtml);
                    string currentTime = _getExchangeRate.GetDate();
                    string systemTime = DateTime.Now.ToString("yyyyMMdd");
                    //if (!currentTime.Equals(systemTime))
                    //{
                    //    // 아직 날짜가 갱신 되지 않았음
                    //    _logger.LogInfoWithCaller($"아직 날짜가 되지 않았습니다: {systemTime} / {currentTime}");
                    //    Console.WriteLine($"아직 날짜가 되지 않았습니다: {systemTime} / {currentTime}");
                    //    continue;
                    //}

                    //_countryExchangeCost.Clear();

                    //_getExchangeRate.FindCost("USD");
                    //_countryExchangeCost.Add("USD", _getExchangeRate.Cost);

                    //_getExchangeRate.FindCost("JPY");
                    //_countryExchangeCost.Add("JPY", _getExchangeRate.Cost * 0.01);

                    //_getExchangeRate.FindCost("EUR");
                    //_countryExchangeCost.Add("EUR", _getExchangeRate.Cost);

                    //_getExchangeRate.FindCost("CNY");
                    //_countryExchangeCost.Add("CNY", _getExchangeRate.Cost);

                    // ui 에 전달
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        WeakReferenceMessenger.Default.Send(new NavigationMessage(typeof(Mainpage)));
                        _mainWindow.Show();
                    });
                }

                _logger.LogInfoWithCaller("ExecuteTimer 종료 됨");
                _isRunning = false;
            }
        }
    }
}
