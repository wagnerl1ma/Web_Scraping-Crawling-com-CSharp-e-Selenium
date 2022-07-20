using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.DesenvolvedorIO
{
    public class DesenvolvedorIOHttpCliente : BaseCrawlerHttpClient
    {
        #region Private - Variáveis    

        private bool LoginSucesso = false;
        private string? Email;
        private string? Senha;
        private string? UrlBase;
        private string? UrlLogin;

        //public static bool _validErro;
        //HtmlDocument? docPagina;
        //HtmlWeb? docPaginaWeb;

        #endregion

        public DesenvolvedorIOHttpCliente()
        {

        }

        public bool LoginDesenvolvedorIO(string? email, string? senha)
        {
            UrlBase = @"https://desenvolvedor.io/";
            UrlLogin = @"https://desenvolvedor.io/entrar"; //POST
            Email = email;
            Senha = senha;

            try
            {
                string htmlPage = string.Empty;
                HtmlDocument docLogin = new HtmlDocument();

                // 1 - Carregar a pagina de Login vazia, para validar se a pagina está carregando corretamente.
                htmlPage = CarregarPagina(UrlLogin);
                docLogin.LoadHtml(htmlPage);

                // 2 - Capturar __RequestVerificationToken para efetuar o login, caso necessário.
                HtmlNodeCollection nodes = docLogin.DocumentNode.SelectNodes("//input [@name='__RequestVerificationToken']");

                // 3 - Criar uma coleção de chave e valor, passando as informações do Login para o POST
                NameValueCollection nvcParametros = new NameValueCollection();
                nvcParametros.Add("__RequestVerificationToken", nodes[0].Attributes["value"].Value.ToString()); //capturar o token do login
                nvcParametros.Add("Email", Email); //sempre pegar o atributo pelo "name" do html
                nvcParametros.Add("Password", Senha); //sempre pegar o atributo pelo "name" do html

                // 4 - Carregar a página novamente passando o conteudo de chave e valor "nvcParametros" na chamado do método e verificar se carregou a pagina com o login efetuado.
                htmlPage = CarregarPagina(UrlLogin, UrlLogin, nvcParametros);
                docLogin.LoadHtml(htmlPage);

                //5 - Criar validação para verificar se o Login está ok 

                //captura a palavra DashBoard para verificar se está logado
                //var contemDashBoardTxt = docLogin.DocumentNode.SelectSingleNode("//a[@title='Meus Cursos']").InnerText; // capturando a tag "a" junto com o title = 'Meus Cursos'

                var validaLogin = ValidarLoginDesenvolvedorIO(htmlPage);

                LoginSucesso = validaLogin;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro no Login! {e.Message}");
                LoginSucesso = false;
            }


            return LoginSucesso;
        }

        public bool ValidarLoginDesenvolvedorIO(string htmlPage) 
        {
            bool boolLogin = true;
            string strMensagem = string.Empty;

            try
            {
                if (htmlPage.Contains("Dashboard"))
                {
                    strMensagem = "Login feito com sucesso!";
                }
                else if (htmlPage.Contains("OPA! ALGO DEU ERRADO"))
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

    }
}
