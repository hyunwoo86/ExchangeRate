using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ExchangeRate.Services
{
    public class GetExchangeRate
    {
        double _cost = 0;
        HtmlDocument _doc = new HtmlDocument();

        public GetExchangeRate()
        {
        }

        public double Cost { get => _cost; set => _cost = value; }

        public void LoadHtml(string html)
        {
            _doc.LoadHtml(html);
        }

        public string GetDate()
        {
            var dateInput = _doc.DocumentNode.SelectSingleNode("//input[@id='searchDate']");
            string searchDate = dateInput?.GetAttributeValue("value", "");
            Console.WriteLine("조회일: " + searchDate);

            return searchDate;
        }

        public void FindCost(string countryName)
        {
            var usdRow = _doc.DocumentNode.SelectNodes("//table[contains(@class, 'tType01')]/tbody/tr")
                ?.FirstOrDefault(tr => tr.InnerText.Contains(countryName));

            if (usdRow != null)
            {
                List<HtmlNode> tds = usdRow.SelectNodes("td")
                    .Where(td => td.GetAttributeValue("class", "").Contains("tRight"))
                    .ToList();

                if (tds.Count > 0)
                {
                    string 기준환율 = tds[0].InnerText.Trim(); // 첫 번째: 매매기준율
                    Console.WriteLine($"{countryName} 매매기준율: " + 기준환율);
                }
            }
            else
            {
                Console.WriteLine($"{countryName} 행을 찾지 못했습니다.");
            }
        }
    }
}
