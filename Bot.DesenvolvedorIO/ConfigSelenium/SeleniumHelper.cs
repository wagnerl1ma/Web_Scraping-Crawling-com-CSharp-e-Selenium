using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
//using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Bot.DesenvolvedorIO.ConfigSelenium
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;
        public WebDriverWait Wait;

        public SeleniumHelper(BrowserEnum browser, ConfigurationHelper configuration, bool headless = true)
        {
            Configuration = configuration;
            WebDriver = WebDriverFactory.CreateWebDriver(browser, Configuration.WebDrivers, headless); //headless = true: navegar de forma invisivel, o browser vai estar aberto mas não é possível ver
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)); // caso nao achar algum elemento na tela ou algum problema de conexao, aguarda 10 segundos.
        }

        public string ObterUrl()
        {
            return WebDriver.Url;
        }

        public void IrParaUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public bool ValidarConteudoUrl(string conteudo)
        {
            //valida se contem um trecho da URL
            return Wait.Until(ExpectedConditions.UrlContains(conteudo));
        }

        public void ClicarLinkPorTexto(string linkText)
        {
            //clica no link que contem o texto na página, preenchido na variavel "linkText"
            var link = Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
            link.Click();
        }

        public void ClicarBotaoPorId(string botaoId)
        {
            var botao = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(botaoId)));
            botao.Click();
        }

        public void ClicarBotaoPorNome(string botaoNome)
        {
            var botao = Wait.Until(ExpectedConditions.ElementIsVisible(By.Name(botaoNome)));
            botao.Click();
        }

        public void ClicarBotaoPorNomeTag(string nomeTag)
        {
            var botao = Wait.Until(ExpectedConditions.ElementIsVisible(By.TagName(nomeTag)));
            botao.Click();
        }

        public void ClicarPorXPath(string xPath)
        {
            var elemento = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
            elemento.Click();
        }

        public string ObterTextoPorXPath(string xPath)
        {
            try
            {
                var elemento = WebDriver.FindElement(By.XPath(xPath)).Text;
                return elemento.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public string ObterValorTextoTela(string nomeTag, string texto)
        {
            var elemento = Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.TagName(nomeTag), texto));
            return elemento.ToString();
        }

        public IWebElement ObterElementoPorClasse(string classeCss)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(classeCss)));
        }

        public IWebElement ObterElementoPorXPath(string xPath)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
        }

        public void PreencherTextBoxPorId(string idCampo, string valorCampo)
        {
            //digita o texto letra por letra no campo
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo)));
            campo.SendKeys(valorCampo);
        }

        public void PreencherTextBoxPorName(string name, string valorCampo)
        {
            //digita o texto letra por letra no campo
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(By.Name(name)));
            campo.SendKeys(valorCampo);
        }

        public void PreencherDropDownPorId(string idCampo, string valorCampo)
        {
            //seleciona e preenche o dropdown
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo)));
            var selectElement = new SelectElement(campo);
            selectElement.SelectByValue(valorCampo);
        }

        public string ObterTextoElementoPorClasseCss(string className)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className))).Text;
        }

        public string ObterTextoElementoPorId(string id)
        {
            try
            {
                //var texto = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id))).Text;
                var texto = WebDriver.FindElement(By.Id(id)).Text;
                return texto.ToString();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }  
        }

        public string ObterValorTextBoxPorId(string id)
        {
            //pega o texbox por id e captura o valor dele
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id))).GetAttribute("value");
        }

        public IEnumerable<IWebElement> ObterListaPorClasse(string className)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(className)));
        }

        public bool ValidarSeElementoExistePorId(string id)
        {
            return ElementoExistente(By.Id(id));
        }

        public bool ValidarSeElementoExistePorNome(string nome)
        {
            return ElementoExistente(By.Name(nome));
        }

        public bool ValidarSeElementoExistePorXPath(string xpatch)
        {
            return ElementoExistente(By.XPath(xpatch));
        }

        public void VoltarNavegacao(int vezes = 1)
        {
            for (var i = 0; i < vezes; i++)
            {
                WebDriver.Navigate().Back();
            }
        }

        public void ObterScreenShot(string nome)
        {
            SalvarScreenShot(WebDriver.TakeScreenshot(), string.Format("{0}_" + nome + ".png", DateTime.Now.ToFileTime()));
        }

        private void SalvarScreenShot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile($"{Configuration.FolderPicture}{fileName}", ScreenshotImageFormat.Png);
        }

        private bool ElementoExistente(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver.Quit(); //fecha o browser
            WebDriver.Dispose();
        }
    }
}
