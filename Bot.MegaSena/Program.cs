using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;
namespace Bot.MegaSena.Html
{
    class Program
    {
        //Pega os dados pelo o HTML
        static void Main(string[] args)
        {
            Console.WriteLine("Informe o numero do concurso: ");
            string numeroDoConcurso = Console.ReadLine();

            //Console.WriteLine(numeroDoConcurso);

            if (string.IsNullOrWhiteSpace(numeroDoConcurso))
            {
                numeroDoConcurso = "2103";
            }

            string url = @"http://www1.caixa.gov.br/loterias/loterias/megasena/megasena_pesquisa_new.asp?submeteu=sim&opcao=concurso&txtConcurso=" + numeroDoConcurso;   //url
            string html;

            using (WebClient wc = new WebClient())               /// usando o using ele não dexa o objeto em memória
            {
                wc.Headers["Cookie"] = "security=true";       //cookie de seguranca
                html = wc.DownloadString(url);   // fazer o download do html
            }

            //limpando o html
            html = html.Replace("<span class=\"num_sorteio\"><ul>", ""); //retira span
            html = html.Replace("</ul></span>", ""); //retira span
            html = html.Replace("</li>", ""); //retira span


            string[] vet = Regex.Split(html, "<li>");
            List<int> resultado = new List<int>();

            resultado.Add(int.Parse(vet[1]));
            resultado.Add(int.Parse(vet[2]));
            resultado.Add(int.Parse(vet[3]));
            resultado.Add(int.Parse(vet[4]));
            resultado.Add(int.Parse(vet[5]));
            resultado.Add(int.Parse(vet[6].Substring(0, 2))); //pegar os 2 primeiros caracteres

            Console.WriteLine("Concurso selecionado: " + numeroDoConcurso);

            Console.WriteLine("Resultado: ");
            Console.WriteLine("-------------------------------------");

            resultado.OrderBy(x => x).ToList().ForEach(num =>                       //para cada resultado ordena pelo menor para o maior e mostrar o valor na tela
            {
                Console.WriteLine(num);
            });


            Console.ReadKey();
        }
    }
}
