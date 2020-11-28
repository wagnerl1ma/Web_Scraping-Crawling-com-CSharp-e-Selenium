using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Bot.OnePiece
{
    public static class InfoOnePiece
    {

        public static Episodios GetEpisodios(string numeroEp)
        {
            var ep = new Episodios();
            string url = @"https://onepieceex.net/episodio-" + numeroEp + "/";   ////https://onepieceex.net/episodio-950/
            string codehtml;


            try
            {
                using (WebClient webClient = new WebClient())
                {

                    codehtml = webClient.DownloadString(url);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            


            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(codehtml);



            var listLink = html.DocumentNode.SelectNodes("//article[@class='episodiov5']");  // capturando a tag article junto com o class = 'episodiov5'

            foreach (var item in listLink)
            {
                var list2 = html.DocumentNode.SelectNodes("//a[@class='online-header']//h1"); // capturando a tag "A"  junto com o class = 'online-header' e o elemento h1
                foreach (var item2 in list2)
                {
                    Console.WriteLine("Inner Text: " + item2.InnerText);

                    var numeroEpisodio = ep.numeroEp = item2.InnerText.Trim();
                    Console.WriteLine("Número do Episódio: " + numeroEpisodio);
                    
                }
            }


            foreach (var item in listLink)
            {
                var list2 = html.DocumentNode.SelectNodes("//a[@class='online-header']//h2"); // capturando a tag "A"  junto com o class = 'online-header' e o elemento h2
                foreach (var item2 in list2)
                {
                    Console.WriteLine("Inner Text: " + item2.InnerText);

                    var episodio = ep.nomeEp = item2.InnerText.Trim();
                    Console.WriteLine("Nome do Episódio: " + episodio);

                }
            }

            return ep;
        }
    }
}
