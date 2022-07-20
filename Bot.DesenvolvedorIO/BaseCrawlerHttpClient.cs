using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;

namespace Bot.DesenvolvedorIO
{
    public class BaseCrawlerHttpClient
    {
        HttpClientHandler? handler = null;
        HttpClient? _httpClient = null;

        public BaseCrawlerHttpClient()
        {
            handler = new HttpClientHandler();
            handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
        }

        public string CarregarPagina(string url, string referer = "", NameValueCollection? valores = null, List<Tuple<string, string>>? valoresList = null, List<KeyValuePair<string, string>>? KeyValueList = null)
        {

            var uri = new Uri(url);

            var request = new HttpRequestMessage();

            if (!string.IsNullOrEmpty(referer))
                request.Headers.Add("Referer", referer);

            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Accept", "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, */*");
            request.Headers.Add("Accept-Language", "pt-BR");

            request.RequestUri = uri;

            _httpClient = new HttpClient(handler);

            if (valores == null && valoresList == null && KeyValueList == null)
            {
                request.Method = HttpMethod.Get;
            }
            else
            {
                request.Method = HttpMethod.Post;

                if (KeyValueList != null)
                {
                    request.Content = new FormUrlEncodedContent(KeyValueList);
                }
                else if (valoresList != null)
                {
                    var valoresConvertidos = ConverterListParaListKeyValue(valoresList);
                    request.Content = new FormUrlEncodedContent(valoresConvertidos);
                }
                else
                {
                    var valoresConvertidos = ConverterNameValueCollectionParaListKeyValue(valores);
                    request.Content = new FormUrlEncodedContent(valoresConvertidos);
                }
            }

            //antes
            //var msg = _httpClient.SendAsync(request);
            //msg.Wait();

            //var html = msg.Result.Content.ReadAsStringAsync();
            //html.Wait();

            //var strHtml = html.Result;

            //return strHtml;


            //depois

            string strHtml = string.Empty;
            HttpResponseMessage? msg = null;

            if (request.Method == HttpMethod.Get)
            {
                msg = _httpClient.GetAsync(url).GetAwaiter().GetResult();

                //var html = msg.Result.Content.ReadAsByteArrayAsync().Result;
                strHtml = msg.Content.ReadAsStringAsync().Result;
            }
            else if (request.Method == HttpMethod.Post) 
            {
                msg = _httpClient.PostAsync(url, request.Content).GetAwaiter().GetResult();
                strHtml = msg.Content.ReadAsStringAsync().Result;
            }


            return strHtml;


            //GetAsync Retorna com UTF8
            //using (HttpClient client = new HttpClient())
            //{
            //    using (HttpResponseMessage response = client.GetAsync(url).Result)
            //    {
            //        var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            //        var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            //        return result;
            //    }
            //}
        }

        public IEnumerable<KeyValuePair<string, string>> ConverterNameValueCollectionParaListKeyValue(NameValueCollection valores)
        {
            return valores.AllKeys.SelectMany(
                valores.GetValues,
                (k, v) => new KeyValuePair<string, string>(k, v));
        }

        public List<KeyValuePair<string, string>> ConverterListParaListKeyValue(IEnumerable<Tuple<string, string>> valores)
        {
            List<KeyValuePair<string, string>> keyvalue = new List<KeyValuePair<string, string>>();
            string[] arr = null;

            foreach (Tuple<string, string> item in valores)
            {
                try
                {
                    if (item.Item1.Contains("DESPACHO"))
                    {
                        arr = item.Item2 == null ? new String[2] : item.Item2.Split('♣');
                    }
                    else
                    {
                        arr = item.Item2 == null ? new String[2] : item.Item2.Split(','); // aqui foi trocado por %2C para a string nao quebrar na virgula
                    }

                    //arr = item.Item2 == null ? new String[2] : item.Item2.Split(','); // aqui foi trocado por %2C para a string nao quebrar na virgula

                    if (arr != null && arr.Length > 0)
                    {
                        foreach (var iarr in arr)
                        {
                            keyvalue.Add(new KeyValuePair<string, string>(item.Item1, iarr));
                        }
                    }
                    else
                    {
                        keyvalue.Add(new KeyValuePair<string, string>(item.Item1, item.Item2));
                    }

                }
                catch (Exception)
                {

                }
            }

            return keyvalue;
        }

    }
}
