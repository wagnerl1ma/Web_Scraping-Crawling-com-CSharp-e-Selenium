using Bot.DesenvolvedorIO;


#region Login Desenvolvedor.IO com Http Cliente

//Console.WriteLine("Login Desenvolvedor.IO com Http Cliente");
//var dadosLogin = DadosLogin();

//var desenvIOHttpCliente = new DesenvolvedorIOHttpCliente();
//desenvIOHttpCliente.LoginDesenvolvedorIO(dadosLogin[0], dadosLogin[1]);

#endregion

#region Login Desenvolvedor.IO com Selenium

Console.WriteLine("Login Desenvolvedor.IO com Selenium");
var dadosLogin = DadosLogin();

var desenvIOSelenium = new DesenvolvedorIO_Selenium();
desenvIOSelenium.LoginDesenvolvedorIO(dadosLogin[0], dadosLogin[1]);

#endregion

 List<string> DadosLogin()
{
    Console.Write("digite seu email:");
    var email = Console.ReadLine();

    Console.Write("digite sua senha:");
    var senha = Console.ReadLine();

    var dados = new List<string>();
    dados.Add(email);
    dados.Add(senha);

    return dados;
}
