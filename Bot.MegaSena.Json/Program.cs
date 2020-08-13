using Newtonsoft.Json;
using System;
using System.Net;

namespace Bot.MegaSena.Json
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Informe o numero do concurso: ");
            string numeroDoConcurso = Console.ReadLine();

            //Console.WriteLine(numeroDoConcurso);

            if (string.IsNullOrWhiteSpace(numeroDoConcurso))
            {
                numeroDoConcurso = "2103";
            }

            string url = @"http://loterias.caixa.gov.br/wps/portal/loterias/landing/megasena/!ut/p/a1/04_Sj9CPykssy0xPLMnMz0vMAfGjzOLNDH0MPAzcDbwMPI0sDBxNXAOMwrzCjA0sjIEKIoEKnN0dPUzMfQwMDEwsjAw8XZw8XMwtfQ0MPM2I02-AAzgaENIfrh-FqsQ9wNnUwNHfxcnSwBgIDUyhCvA5EawAjxsKckMjDDI9FQE-F4ca/dl5/d5/L2dBISEvZ0FBIS9nQSEh/pw/Z7_HGK818G0KO6H80AU71KG7J0072/res/id=buscaResultado/c=cacheLevelPage/=/?timestampAjax=1596205516158&concurso=" + numeroDoConcurso;   //url //2283
            string json;

            using (WebClient wc = new WebClient())               /// usando o using ele não dexa o objeto em memória
            {
                wc.Headers["Cookie"] = "security=true";       //cookie de seguranca
                json = wc.DownloadString(url);   // fazer o download 
            }

            var resultadoMegaSena = JsonConvert.DeserializeObject<Resultado>(json);         //fazer a leitura do json e insere os resultados na var resultadoMegaSena //transforma a string em um objeto

            Console.WriteLine("Concurso selecionado: " + numeroDoConcurso);

            Console.WriteLine("Resultado: ");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine(resultadoMegaSena.resultadoOrdenado);

            Console.ReadKey();
        }
    }
}
