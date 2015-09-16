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
    class XiaoMiHttpClient
    {
        long clientId;
        string clientKey;
        /**
         * 构造方法
         * @param clientId为客户端
         * @param clientKey为id密码
         * @return 
         */
        public XiaoMiHttpClient(long clientId, String clientKey)
        {
            this.clientId = clientId; 
            this.clientKey = clientKey;
        }
        /**
         * 获得权限的uri
         * @param redirectUri返回uri
         * @param responseType返回类型填token或code
         * @param scope 权限，没有为null
         * @param state 可选传参，没有为null
         * @param skipConfirm是否为黄页，默认为false
         * @return 权限的uri
         */
        public String getAuthorizeURL(String redirectUri, String responseType, String scope, String state, bool skipConfirm)
        {
            UriBuilderImprove urlBuilder = new UriBuilderImprove(XiaoMiHttpClientConst.authorizeURI);
            urlBuilder.QueryString[XiaoMiHttpClientConst.clientIdName] = clientId.ToString();
            urlBuilder.QueryString[XiaoMiHttpClientConst.redirectUriName] = redirectUri;
            urlBuilder.QueryString[XiaoMiHttpClientConst.responseTypeName] = responseType;
            if (scope != null) urlBuilder.QueryString[XiaoMiHttpClientConst.scopeeName] = scope;
            if (state != null) urlBuilder.QueryString[XiaoMiHttpClientConst.stateName] = state; 
            if (skipConfirm) urlBuilder.QueryString[XiaoMiHttpClientConst.skipConfirmName] = skipConfirm.ToString();
            return  urlBuilder.ToString();
        }
        /**
         * 刷新token
         * @param  redirectUri返回uri
         * @param  grantType使用类型填refresh_token或authorization_code
         * @param  value为使用的类型的值
         * @return 得到的token
         */
        public String getRefreshTokenURL(String redirectUri, String grantType, String val)
        {
            UriBuilderImprove urlBuilder = new UriBuilderImprove(XiaoMiHttpClientConst.tokenURI);
            urlBuilder.QueryString[XiaoMiHttpClientConst.clientIdName] = clientId.ToString();
            urlBuilder.QueryString[XiaoMiHttpClientConst.redirectUriName] = redirectUri;
            urlBuilder.QueryString[XiaoMiHttpClientConst.clientSecretName] = clientKey;
            if (grantType.Equals(XiaoMiHttpClientConst.refreshTokenName))
            {
                urlBuilder.QueryString[XiaoMiHttpClientConst.grantTypeName] = XiaoMiHttpClientConst.refreshTokenName;
                urlBuilder.QueryString[XiaoMiHttpClientConst.refreshTokenName] =  val;
            }
            else
            {
                urlBuilder.QueryString[XiaoMiHttpClientConst.grantTypeName] = XiaoMiHttpClientConst.authorizationCodeName;
                urlBuilder.QueryString[XiaoMiHttpClientConst.codeName] = val;
            }
            return urlBuilder.ToString();
        }
       
    }
}
