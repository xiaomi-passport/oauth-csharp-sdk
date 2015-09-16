using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using System.Data;
using System.Net.Security;
using System.Security.Cryptography;

namespace XiaoMiOauth2
{
    class HttpClient
    {
        public static string getResponse(string uri, string method, Dictionary<string,string> headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = method;
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> headersKey in headers)
                {
                    request.Headers.Add(headersKey.Key, headersKey.Value);
                }
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string respHtml;
            try
            {
                respHtml = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                reader.Close();
            }
            return respHtml;

        }
    }
}
