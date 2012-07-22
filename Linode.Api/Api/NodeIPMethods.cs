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
    public static class NodeIPMethods
    {
        /// <summary>
        /// Adds a new Private IP to the Linode
        /// </summary>
        /// <param name="linodeId">The Id of the Linode to add the IP to</param>
        /// <param name="apiKey">The Api Key of the User</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void Add(int linodeId, string apiKey, Action<Response<IpResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_IP_ADDPRIVATE, req_dict);

            var httpClient = new HttpClient<IpResponse>(req, new Action<Response<IpResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Get a List of IP Addresses Assigned to the Given Linode
        /// </summary>
        /// <param name="linodeId">The Id of the Linode</param>
        /// <param name="ipAddressId">The Id of the IP Address (if you want to filter the results)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to pass the response to</param>
        public static void List(int linodeId, int? ipAddressId, string apiKey, Action<Response<Ip[]>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());

            if (ipAddressId.HasValue && ipAddressId.Value > 0)
                req_dict.Add("IPAddressID", ipAddressId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_IP_LIST, req_dict);

            var httpClient = new HttpClient<Ip[]>(req, new Action<Response<Ip[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
    }
}
