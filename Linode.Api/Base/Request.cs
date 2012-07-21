using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linode.Api.Base
{
    /// <summary>
    /// Represents a Request to be sent to Linode
    /// </summary>
    public class Request
    {
        private static readonly string BaseUrlPattern = "https://api.linode.com/?";

        #region Constructors

        /// <summary>
        /// Creates a new Request Object, using the given action and parameters 
        /// Use this Constructor for Unauthenticated Requests
        /// </summary>
        /// <param name="action">The Action to be executed</param>
        /// <param name="parameters">The Parameters to be sent</param>
        public Request(String action, Dictionary<string, string> parameters = null)
        {
            this.ApiKey = string.Empty;
            this.Action = action;
            Parameters = parameters ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates a new Request Object using the given api key, action and parameters
        /// Use this Constructor for Authenticated Requests
        /// </summary>
        /// <param name="apiKey">The API Key for the User</param>
        /// <param name="action">The Action to be executed</param>
        /// <param name="parameters">The Parameters to be sent</param>
        public Request(String apiKey, String action, Dictionary<string, string> parameters = null)
        {
            this.ApiKey = apiKey;
            this.Action = action;
            Parameters = parameters ?? new Dictionary<string, string>();
        }

        #endregion

        /// <summary>
        /// The Api Key to be used in this Request
        /// </summary>
        public String ApiKey { get; private set; }
        
        /// <summary>
        /// The Action to be executed in this Request 
        /// </summary>
        public String Action { get; private set; }

        /// <summary>
        /// The Parameters to be sent along with the Request
        /// </summary>
        public Dictionary<String, String> Parameters { get; private set; }

        /// <summary>
        /// Get the Request URL For this Action
        /// </summary>
        /// <returns>A Properly Encoded URL that can be executed</returns>
        public String GetRequestUrl()
        {
            bool first = true;

            StringBuilder sb = new StringBuilder();
            sb = sb.Append(BaseUrlPattern);

            if (!String.IsNullOrEmpty(ApiKey))
            {
                sb = sb.Append("api_key=").Append(ApiKey);
                first = false;
            }

            var action = "api_action=" + Action;
            if (first)
            {
                sb = sb.Append(action);
                first = false;
            }
            else
            {
                sb = sb.Append("&").Append(action);
            }

            foreach (var kvp in Parameters)
            {
                if (!first)
                {
                    sb = sb.Append("&");
                }
                sb = sb.Append(kvp.Key).Append("=").Append(Uri.EscapeUriString(kvp.Value));
                first = false;
            }

            return sb.ToString();
        }
    }
}
