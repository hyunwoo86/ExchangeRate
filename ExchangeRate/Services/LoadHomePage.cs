using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ExchangeRate.Services
{
    public class LoadHomePage
    {
        string _fullHtml = string.Empty;

        public LoadHomePage()
        {
        }
        public string FsullHtml { get => _fullHtml; set => _fullHtml = value; }

        public bool Load()
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;  // ✅ CMD 창 숨김

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");                  // 💡 창 안 뜨게 실행
            options.AddArgument("--disable-gpu");               // GPU 렌더링 비활성화
            options.AddArgument("--no-sandbox");                // (리눅스 안전 모드 해제용)
            //options.AddArgument("--window-size=1920,1080");     // 가상 화면 사이즈 설정

            using var driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("https://obank.kbstar.com/quics?page=C101423#loading");
            Thread.Sleep(7000);

            try
            {
                var radioInit = driver.FindElement(By.Id("inp-radio1-2"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", radioInit);
                Thread.Sleep(1000);

                var searchButton = driver.FindElement(By.CssSelector("button.btn-com.c2"));
                searchButton.Click();
                Thread.Sleep(1000);

                string fullHtml = driver.PageSource;
                FsullHtml = fullHtml;
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류 발생: " + ex.Message);
                driver.Quit();
                return false;
            }

            driver.Quit();
            return true;
        }
    }
}
