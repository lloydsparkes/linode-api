using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linode.Api.Base;
using Linode.Api.Linode;
using Linode.Api.Reference;
using Linode.Api.Utility;

namespace Linode.Api
{
    public static class LinodeClient
    {
        #region Login & Check Auth Methods

        /// <summary>
        /// Check to see if a Username & Password Combination is valid and get a API Key
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="responseAction">The Action to Callback to, with the response</param>
        public static void Login(string username, string password, Action<Response<ApiKey>> responseAction)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("password");

            var req_dict = new Dictionary<string,string>();
            req_dict.Add("username", username);
            req_dict.Add("password", password);

            var req = new Request(LinodeActions.USER_GETAPIKEY, req_dict);

            var httpClient = new HttpClient<ApiKey>(req, new Action<Response<ApiKey>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Gets the Account Information - Including Active From and Bandwidth Pool Information
        /// Can Also be used for validating an api key
        /// </summary>
        /// <param name="apiKey">The Users API Key</param>
        /// <param name="responseAction">The Action to Callback to, with the response</param>
        public static void GetAccountInformation(string apiKey, Action<Response<AccountInformation>> responseAction)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey");

            var req = new Request(apiKey, LinodeActions.ACCOUNT_INFO, new Dictionary<string,string>());

            var httpClient = new HttpClient<AccountInformation>(req, new Action<Response<AccountInformation>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #endregion

        #region Linode Actions

        /// <summary>
        /// Get a List of Linodes - Optionally Restricted to a Specific Linode Id
        /// </summary>
        /// <param name="linodeId">Optional - Restrict the list of Linodes to a single Linode</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">To action to callback to with the response</param>
        public static void GetLinodes(int? linodeId, string apiKey, Action<Response<Node[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();
            if(linodeId.HasValue && linodeId.Value > 0)
                req_dict.Add("LinodeID", linodeId.Value.ToString());

            var req = new Request(LinodeActions.LINODE_LIST, req_dict);

            var httpClient = new HttpClient<Node[]>(req, new Action<Response<Node[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #region Boot, Reboot Shutdown

        /// <summary>
        /// Boot a Linode
        /// </summary>
        /// <param name="linodeId">The Linode ID</param>
        /// <param name="configId">The Configuration ID (optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return to with the response</param>
        public static void BootLinode(int linodeId, int? configId, string apiKey, Action<Response<JobResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (configId.HasValue && configId.Value <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();

            req_dict.Add("LinodeID", linodeId.ToString());

            if (configId.HasValue)
                req_dict.Add("ConfigID", configId.Value.ToString());

            var req = new Request(LinodeActions.LINODE_BOOT, req_dict);

            var httpClient = new HttpClient<JobResponse>(req, new Action<Response<JobResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        /// <summary>
        /// Reboots a Linode
        /// </summary>
        /// <param name="linodeId">The Linode ID</param>
        /// <param name="configId">The Configuration ID (optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return to with the response</param>
        public static void RebootLinode(int linodeId, int? configId, string apiKey, Action<Response<JobResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (configId.HasValue && configId.Value <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();

            req_dict.Add("LinodeID", linodeId.ToString());

            if (configId.HasValue)
                req_dict.Add("ConfigID", configId.Value.ToString());

            var req = new Request(LinodeActions.LINODE_REBOOT, req_dict);

            var httpClient = new HttpClient<JobResponse>(req, new Action<Response<JobResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        /// <summary>
        /// Shutdown a Linode
        /// </summary>
        /// <param name="linodeId">The Linode ID</param>
        /// <param name="configId">The Configuration ID (optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return to with the response</param>
        public static void ShutdownLinode(int linodeId, string apiKey, Action<Response<JobResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();

            req_dict.Add("LinodeID", linodeId.ToString());

            var req = new Request(LinodeActions.LINODE_SHUTDOWN, req_dict);

            var httpClient = new HttpClient<JobResponse>(req, new Action<Response<JobResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        #endregion

        #endregion
    }
}
