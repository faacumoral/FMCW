using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FMCW.Common.HTTP
{
    public class ProxyHandler
    {
        //private readonly string _apiUrl;
        //private readonly string _apiKey;
 
        //public ProxyHandler()
        //{
        //    _apiUrl = GetConfig(Constants.Config.API_URL);
        //    _apiKey = GetConfig(Constants.Config.API_KEY);
        //}

        public T ApiGetRequest<T>(string Url,
            string ContentType = "",
            string EncodeType = "utf-8",
            string QueryString = "",
            Dictionary<string, string> Headers = null)
        {
            //if (!Url.Contains(_apiUrl))
            //{
            //    Url = _apiUrl + Url;
            //}
            return DoRequest<T>(Url +  QueryString, "GET", ContentType, EncodeType, string.Empty, Headers);
        }

        public Tresult ApiPostRequest<Tresult, Tparam>(string Url,
            string ContentType = "",
            string EncodeType = "utf-8",
            Tparam Params = null,
            Dictionary<string, string> Headers = null) where Tparam : class
        {
            //if (!Url.Contains(_apiUrl))
            //{
            //    Url = _apiUrl + Url;
            //}
            return DoRequest<Tresult>(Url, "POST", ContentType, EncodeType, Params == null ? string.Empty : JsonConvert.SerializeObject(Params), Headers);
        }

        T DoRequest<T>(string Url,
            string Method = "GET",
            string ContentType = "",
            string EncodeType = "utf-8",
            string Params = null,
            Dictionary<string, string> Headers = null)
        {
            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request;
            request = WebRequest.Create(Url) as HttpWebRequest;
            request.Method = Method;
            request.ContentType = ContentType;
            request.ServerCertificateValidationCallback = (message, cert, chain, errors) => true;
            
            //drequest.Headers.Add("api-key", _apiKey);

            if (Headers != null)
            {
                foreach (var h in Headers)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }

            if (Method == "POST" && !string.IsNullOrEmpty(Params))
            {
                request.ContentLength = Params.Length;
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(Params);
                requestWriter.Close();
            }

            StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding(EncodeType));
            string json = responseReader.ReadToEnd();
            responseReader.Close();
            // request.GetResponse().Close();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}