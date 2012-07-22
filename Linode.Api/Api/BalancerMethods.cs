using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linode.Api.Base;
using Linode.Api.Linode;
using Linode.Api.Balancer;
using Linode.Api.Utility;

namespace Linode.Api
{
    public static class BalancerMethods
    {

        /// <summary>
        /// List of Node Balancers
        /// </summary>
        /// <param name="nodeBalancerId">NodeBalancer Id to Filter By</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void List(int? nodeBalancerId, string apiKey, Action<Response<Balancer.Balancer[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();
            if (nodeBalancerId.HasValue && nodeBalancerId.Value > 0)
                req_dict.Add("NodeBalancerID", nodeBalancerId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_LIST, req_dict);

            var httpClient = new HttpClient<Balancer.Balancer[]>(req, new Action<Response<Balancer.Balancer[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Delete a Node Balancer
        /// </summary>
        /// <param name="nodeBalancerId">NodeBalancer Id to Delete</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Delete(int nodeBalancerId, string apiKey, Action<Response<BalancerResponse>> responseAction)
        {
            if (nodeBalancerId <= 0)
                throw new ArgumentOutOfRangeException("nodeBalancerId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("NodeBalancerID", nodeBalancerId.ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_DELETE, req_dict);

            var httpClient = new HttpClient<BalancerResponse>(req, new Action<Response<BalancerResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Create a new Node Balancer
        /// </summary>
        /// <param name="dataCenterId">DataCenter Id</param>
        /// <param name="paymentTerm">Payment Term</param>
        /// <param name="apiKey">The Users API Key</param>
        /// <param name="responseAction">The action the response is sent to</param>
        public static void Create(int dataCenterId, PaymentTerm paymentTerm, string apiKey, Action<Response<BalancerResponse>> responseAction)
        {
            if (dataCenterId <= 0)
                throw new ArgumentOutOfRangeException("dataCenterId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DatacenterID", dataCenterId.ToString());
            req_dict.Add("PaymentTerm", ((int)paymentTerm).ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_CREATE, req_dict);

            var httpClient = new HttpClient<BalancerResponse>(req, new Action<Response<BalancerResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Update a Node Balancer
        /// </summary>
        /// <param name="nodeBalancerId">The Id of the Balancer</param>
        /// <param name="label">The New Name (Optional)</param>
        /// <param name="clientConnThrottle">ClientConnectionThrottle - 0 Disabled -> Max 20 (Optional)</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Update(int nodeBalancerId, string label, int? clientConnThrottle, string apiKey, Action<Response<BalancerResponse>> responseAction)
        {
            if (nodeBalancerId <= 0)
                throw new ArgumentOutOfRangeException("nodeBalancerId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("NodeBalancerID", nodeBalancerId.ToString());

            if (!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if (clientConnThrottle.HasValue)
                req_dict.Add("ClientConnThrottle", clientConnThrottle.Value.ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_UPDATE, req_dict);

            var httpClient = new HttpClient<BalancerResponse>(req, new Action<Response<BalancerResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
    }
}
