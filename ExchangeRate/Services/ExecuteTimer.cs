using ExchangeRate.Common;
using Microsoft.Extensions.Logging;

namespace ExchangeRate.Services
{
    public class ExecuteTimer
    {
        // https://obank.kbstar.com/quics?page=C101423#loading

        LoadHomePage _loadHomePage;
        GetExchangeRate _getExchangeRate;
        Dictionary<string, double> _countryExchangeCost = new Dictionary<string, double>();
        private readonly ILogger<ExecuteTimer> _logger;

        public ExecuteTimer(LoadHomePage loadHomePage, GetExchangeRate getExchangeRate, ILogger<ExecuteTimer> logger)
        {
            _logger = logger;
            _loadHomePage = loadHomePage;
            _getExchangeRate = getExchangeRate;
        }

        public void Execute()
        {
            _logger.LogInfoWithCaller("ExecuteTimer 시작됨");

            if (_loadHomePage.Load())
            {
                _getExchangeRate.LoadHtml(_loadHomePage.FsullHtml);
                string currentTime = _getExchangeRate.GetDate();
                string systemTime = DateTime.Now.ToString("yyyyMMdd");
                if (!currentTime.Equals(systemTime))
                {
                    // 아직 날짜가 갱신 되지 않았음
                    _logger.LogInfoWithCaller($"아직 날짜가 되지 않았습니다: {systemTime} / {currentTime}");
                    return;
                }

                _countryExchangeCost.Clear();

                _getExchangeRate.FindCost("USD");
                _countryExchangeCost.Add("USD", _getExchangeRate.Cost);

                _getExchangeRate.FindCost("JPY");
                _countryExchangeCost.Add("JPY", _getExchangeRate.Cost * 0.01);

                _getExchangeRate.FindCost("EUR");
                _countryExchangeCost.Add("EUR", _getExchangeRate.Cost);

                _getExchangeRate.FindCost("CNY");
                _countryExchangeCost.Add("CNY", _getExchangeRate.Cost);
            }

            _logger.LogInfoWithCaller("ExecuteTimer 종료 됨"); 
        }
    }
}
