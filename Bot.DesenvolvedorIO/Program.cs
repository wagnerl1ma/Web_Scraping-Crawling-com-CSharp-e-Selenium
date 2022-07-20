using Bot.DesenvolvedorIO;

// Teste de Login Desenvolvedor.IO com Http Cliente
Console.WriteLine("Login Desenvolvedor.IO com Http Cliente");

Console.Write("digite seu email:");
var email = Console.ReadLine();

Console.Write("digite sua senha:");
var senha = Console.ReadLine();

var desenvIOHttpCliente = new DesenvolvedorIOHttpCliente();
desenvIOHttpCliente.LoginDesenvolvedorIO(email, senha);

//TODO: Fazer Teste de Login Desenvolvedor.IO com Selenium