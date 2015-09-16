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
using System.Collections.Specialized;

namespace XiaoMiOauth2
{
    public class UriBuilderImprove : UriBuilder
    {
        Dictionary<string, string> queryString = null;

        #region Properties
        public Dictionary<string, string> QueryString
        {
            get
            {
                if (queryString == null)
                {
                    queryString = new Dictionary<string, string>();
                }
                return queryString;
            }
        }

        public string PageName
        {
            get
            {
                string path = base.Path;
                return path.Substring(path.LastIndexOf("/") + 1);
            }
            set
            {
                string path = base.Path;
                path = path.Substring(0, path.LastIndexOf("/"));
                base.Path = string.Concat(path, "/", value);
            }
        }
        #endregion

        #region Constructor overloads
        public UriBuilderImprove():base()
        {
        }

        public UriBuilderImprove(string uri):base(uri)
        {
            PopulateQueryString();
        }

        public UriBuilderImprove(Uri uri): base(uri)
        {
            PopulateQueryString();
        }

        public UriBuilderImprove(string schemeName, string hostName): base(schemeName, hostName)
        {
        }

        public UriBuilderImprove(string scheme, string host, int portNumber): base(scheme, host, portNumber)
        {
        }

        public UriBuilderImprove(string scheme, string host, int port, string pathValue): base(scheme, host, port, pathValue)
        {
        }

        public UriBuilderImprove(string scheme, string host, int port, string path, string extraValue): base(scheme, host, port, path, extraValue)
        {
        }
        #endregion

        #region Public methods
        public new string ToString()
        {
            GetQueryString();

            return base.Uri.AbsoluteUri;
        }

        #endregion

        #region Private methods
        private void PopulateQueryString()
        {
            string query = base.Query;

            if (query == string.Empty || query == null)
            {
                return;
            }

            if (queryString == null)
            {
                queryString = new Dictionary<string, string>();
            }

            queryString.Clear();

            query = query.Substring(1); //remove the ?

            string[] pairs = query.Split(new char[] { '&' });
            foreach (string s in pairs)
            {
                string[] pair = s.Split(new char[] { '=' });

                queryString[pair[0]] = (pair.Length > 1) ? pair[1] : string.Empty;
            }
        }

        private void GetQueryString()
        {
            int count = queryString.Count;

            if (count == 0)
            {
                base.Query = string.Empty;
                return;
            }
            string[] keys = new string[count];
            string[] values = new string[count];
            string[] pairs = new string[count];
            queryString.Keys.CopyTo(keys, 0);
            queryString.Values.CopyTo(values, 0);
            for (int i = 0; i < count; i++)
            {
                pairs[i] = string.Concat(keys[i], "=", Uri.EscapeDataString(values[i]));
            }

            base.Query = string.Join("&", pairs);
        }
        #endregion
    }
}
