using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Bot.DesenvolvedorIO.ConfigSelenium
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(BrowserEnum browser, string caminhoDriver, bool headless) //headless: navegar de forma invisivel, o browser vai estar aberto mas não é possível ver
        {
            IWebDriver? webDriver = null;

            switch (browser)
            {
                case BrowserEnum.Edge:
                    var optionsEdge = new EdgeOptions();
                    if (headless)
                        optionsEdge.AddArgument("--headless");

                    webDriver = new EdgeDriver(caminhoDriver, optionsEdge);

                    break;
                case BrowserEnum.Chrome:
                    var options = new ChromeOptions();
                    if (headless)
                        options.AddArgument("--headless");

                    webDriver = new ChromeDriver(caminhoDriver, options);

                    break;
            }

            return webDriver;
        }
    }
}
