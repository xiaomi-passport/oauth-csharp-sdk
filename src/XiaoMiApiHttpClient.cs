using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XiaoMiOauth2
{
    class XiaoMiApiHttpClient
    {
        long clientId;
        string clientKey;
        public XiaoMiApiHttpClient(long clientId, String clientKey)
        {
            this.clientId = clientId; 
            this.clientKey = clientKey;
        }
        /**
        * HMACSHA1加密后为api操作得到用户信息等
        * @param  method http方法，填“GET”或“POST”
        * @param  localPath使用参数详情看sdk文档
        * @param  macKey，hmacsha1的密钥
        * @param  accessToken访问令牌
        * @return 用户信息
        */
        public String apiCall(string method, string localPath, string macKey, string accessToken)
        {
            UriBuilderImprove urlBuilder = new UriBuilderImprove(XiaoMiHttpClientConst.apiURI);
            urlBuilder.Path = localPath;
            urlBuilder.QueryString["clientId"] = clientId.ToString();
            urlBuilder.QueryString["token"] = accessToken;
            urlBuilder.ToString();
            method = method.ToUpperInvariant();
            string nonce = XiaoMiHttpClientUtils.getNonce();
            string query = urlBuilder.Query.Substring(1);
            Console.WriteLine(XiaoMiHttpClientConst.apiURI + localPath + "?" + query);
            string ciphertext = XiaoMiHttpClientUtils.createCiphertext(nonce, query, method, localPath, macKey);
            string header = XiaoMiHttpClientUtils.createHeader(nonce, ciphertext, accessToken);
            //http传输过程
            Dictionary<string, string> headermap = new Dictionary<string, string>();
            headermap.Add(XiaoMiHttpClientConst.authorizationName, header);
            string respHtml = HttpClient.getResponse(urlBuilder.ToString(), method, headermap);
            return respHtml;
        }
    }
}
