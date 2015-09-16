using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace XiaoMiOauth2
{
    class XiaoMiHttpClientTest
    {
        static void Main()
        {
            UriBuilderImprove uriBuilder = new UriBuilderImprove("http://www.baidu.com");
            uriBuilder.Path = "s";
            uriBuilder.QueryString["wd"] = "小米";
            System.Console.WriteLine(HttpClient.getResponse(uriBuilder.ToString(), "GET", null));//测试httpclient能正常返回界面
            long clientId = 179887661252608;
            String clientKey = "50S2mk2MTDHQFbV6O6kMjg==", clientRedirectUri = "http://xiaomi.com";
            XiaoMiHttpClient httpClient = new XiaoMiHttpClient(clientId, clientKey);
            /**
             * getAuthorizeURL获得权限的uri
             * @param redirect_uri返回uri
             * @param response_type返回类型填token或code
             * @param scope 权限，没有为null
             * @param state 可选传参，没有为null
             * @param skip_confirm是否为黄页，默认为false
             * @return 权限的uri
             */
            string getTokenURI = httpClient.getAuthorizeURL(clientRedirectUri, "token", null, null, false);
            System.Console.WriteLine(getTokenURI);
            System.Console.WriteLine(HttpClient.getResponse(getTokenURI, "GET", null));//能正常返回界面
            string refresh_token="eJxjYGAQSS_9M8X40acUv_lnpTc3sjO8O5eRzMDAwMgQDyQZ0pakngLRwTeSwDSjvaghA8Pi2TFqIB4Du6GCkYKxggmQyVuUmlaUWpwRX5KfnZoHALQ_GGY";
            /**
             * getRefreshTokenURL为刷新token的uri
             * @param redirect_uri返回uri
             * @param  grant_type使用类型填refresh_token或authorization_code
             * @param  parameter为使用的类型的值
             * @return 得到的token
             */
            string getRefreshTokenURI = httpClient.getRefreshTokenURL(clientRedirectUri, "refresh_token", refresh_token);
            System.Console.WriteLine(getRefreshTokenURI);
            string HTTP="GET";
            string URI = "/user/relation";
            string mac_key = "b22OmhVgdRIVEPSWAeCyWaEA7GA";
            string token = "eJxjYGAQqVSyKaqq1FK_NFfUU2Yb99pj4uoXGBgYGBnigSRDiHOWBYgOPnMYTDPaixoyMCyeHaMG4jGwGyoYKRgrmACZzLmJyQDggRAr";
            string QUERY = "clientId=" + clientId + "&token=" + token;
            XiaoMiApiHttpClient apiHttpClient = new XiaoMiApiHttpClient(clientId, clientKey);
            /**
             * apiHttpClient.apiCall为api操作得到用户信息等
             * @param  method http方法，填“GET”或“POST”
             * @param  local_path使用参数详情看sdk文档
             * @param  mac_key，hmacsha1的密钥
             * @param  access_token访问令牌
             * @return 用户信息
             */
            string apiResponse = apiHttpClient.apiCall(HTTP, URI, mac_key, token);
            System.Console.WriteLine(apiResponse);
            System.Console.WriteLine(apiResponse.IndexOf("成功") != -1);

             
        }
    }
}