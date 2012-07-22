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
    public static class UtilityMethods
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

            var req_dict = new Dictionary<string, string>();
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

            var req = new Request(apiKey, LinodeActions.ACCOUNT_INFO, new Dictionary<string, string>());

            var httpClient = new HttpClient<AccountInformation>(req, new Action<Response<AccountInformation>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #endregion

        #region Avail Methods

        public static void GetLinodePlans(Action<Response<Plan[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();

            var req = new Request(LinodeActions.LINODEPLANS, req_dict);

            var httpClient = new HttpClient<Plan[]>(req, new Action<Response<Plan[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        public static void GetDataCenters(Action<Response<DataCenter[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();

            var req = new Request(LinodeActions.DATACENTERS, req_dict);

            var httpClient = new HttpClient<DataCenter[]>(req, new Action<Response<DataCenter[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        public static void GetDistributions(Action<Response<Distribution[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();

            var req = new Request(LinodeActions.DISTRIBUTIONS, req_dict);

            var httpClient = new HttpClient<Distribution[]>(req, new Action<Response<Distribution[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        public static void GetKernels(Action<Response<Kernel[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();

            var req = new Request(LinodeActions.KERNELS, req_dict);

            var httpClient = new HttpClient<Kernel[]>(req, new Action<Response<Kernel[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Gets a list of Public StackScripts
        /// </summary>
        /// <param name="distributionId">Optional - Filter by Distribution ID</param>
        /// <param name="distributionVendor">Optional - Filter by Distribution Vendor</param>
        /// <param name="keywords">Optional - Search Keywords</param>
        /// <param name="responseAction"></param>
        public static void GetStackScripts(int? distributionId, string distributionVendor, string keywords, Action<Response<Script[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();

            if (distributionId.HasValue && distributionId.Value > 0)
                req_dict.Add("DistributionID", distributionId.Value.ToString());

            if (!string.IsNullOrEmpty(distributionVendor))
                req_dict.Add("DistributionVendor", distributionVendor);

            if (!string.IsNullOrEmpty(keywords))
                req_dict.Add("keywords", keywords);

            var req = new Request(LinodeActions.STACKSCRIPTS, req_dict);

            var httpClient = new HttpClient<Script[]>(req, new Action<Response<Script[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #endregion
    }
}
