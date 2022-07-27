using Bot.DesenvolvedorIO.ConfigSelenium;

namespace Bot.DesenvolvedorIO
{
    public class DesenvolvedorIO_Selenium
    {
        #region Private - Variáveis    

        private bool LoginSucesso = false;

        #endregion

        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper ConfigurationHelper;

        public DesenvolvedorIO_Selenium()
        {
            BrowserHelper = new SeleniumHelper(BrowserEnum.Chrome, new ConfigurationHelper(), false);
            ConfigurationHelper = new ConfigurationHelper();
        }

        public bool LoginDesenvolvedorIO(string email, string senha)
        {
            try
            {
                //var browser = new SeleniumHelper(BrowserEnum.Chrome, new ConfigurationHelper(), false);
                //browser.PreencherTextBoxPorId("Email", email);

                BrowserHelper.IrParaUrl(ConfigurationHelper.DomainUrl);  //https://desenvolvedor.io/

                BrowserHelper.ClicarLinkPorTexto("Entrar");

                BrowserHelper.PreencherTextBoxPorName("Email", email);

                BrowserHelper.PreencherTextBoxPorName("Password", senha);

                BrowserHelper.ClicarBotaoPorNomeTag("button"); // Clicando no Botão de Login para entrar

                //Validação de Login
                LoginSucesso = ValidarLoginDesenvolvedorIO();
             
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro no Login! {e.Message}");
                LoginSucesso = false;
            }

            return LoginSucesso;
        }

        public bool ValidarLoginDesenvolvedorIO()
        {
            bool boolLogin = true;
            string strMensagem = string.Empty;

            var contemDashBoardTxt = BrowserHelper.ObterTextoPorXPath("//a[@title='Meus Cursos']");// capturando a tag "a" junto com o title = 'Meus Cursos'
            var capturaTextoErroLogin = BrowserHelper.ObterTextoElementoPorId("msgRetorno");
            //var contemLinkDashboard = BrowserHelper.ValidarConteudoUrl(ConfigurationHelper.DashBoard); // https://desenvolvedor.io/dashboard

            try
            {
                if (contemDashBoardTxt.Contains("Dashboard"))
                {
                    strMensagem = "Login feito com sucesso!";
                }
                else if (capturaTextoErroLogin.Contains("OPA! ALGO DEU ERRADO"))
                {
                    boolLogin = false;
                    strMensagem = "Problemas no Login: Usuário ou Senha incorretos";
                }
                else
                {
                    boolLogin = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro no Login! {e.Message}");
                boolLogin = false;
            }

            Console.WriteLine(strMensagem);
            return boolLogin;
        }

        public int ApenasNumeros(string value)
        {
            return Convert.ToInt16(new string(value.Where(char.IsDigit).ToArray()));
        }

    }
}
