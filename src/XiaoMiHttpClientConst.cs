using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XiaoMiOauth2
{
    class XiaoMiHttpClientConst
    {
        public const String authorizeURI = "https://account.xiaomi.com/oauth2/authorize";
        public const String tokenURI = "https://account.xiaomi.com/oauth2/token";
        public const String apiURI = "https://open.account.xiaomi.com";
        public const String hostURI = "open.account.xiaomi.com";
        public const String clientIdName = "client_id";
        public const String redirectUriName = "redirect_uri";
        public const String responseTypeName = "response_type";
        public const String scopeeName = "scope";
        public const String stateName = "state";
        public const String skipConfirmName = "skip_confirm";
        public const String authorizationName = "Authorization";
        public const String clientSecretName = "client_secret";
        public const String refreshTokenName = "refresh_token";
        public const String grantTypeName = "grant_type";
        public const String authorizationCodeName = "authorization_code";
        public const String codeName = "code";
    }
}
