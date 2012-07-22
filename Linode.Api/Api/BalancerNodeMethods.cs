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
    public static class BalancerNodeMethods
    {
        /// <summary>
        /// List of Nodes in a Balancers Config
        /// </summary>
        /// <param name="configId">Config Id</param>
        /// <param name="nodeId">Node Id to Filter By</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void List(int configId, int? nodeId, string apiKey, Action<Response<BalancerNode[]>> responseAction)
        {
            if (configId <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("ConfigID", configId.ToString());
            if (nodeId.HasValue && nodeId.Value > 0)
                req_dict.Add("NodeID", nodeId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_NODE_LIST, req_dict);

            var httpClient = new HttpClient<BalancerNode[]>(req, new Action<Response<BalancerNode[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Delete a Node from a Balancers Config
        /// </summary>
        /// <param name="nodeBalancerId">Node Id to Delete</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Delete(int nodeId, string apiKey, Action<Response<NodeResponse>> responseAction)
        {
            if (nodeId <= 0)
                throw new ArgumentOutOfRangeException("nodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("NodeID", nodeId.ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_NODE_DELETE, req_dict);

            var httpClient = new HttpClient<NodeResponse>(req, new Action<Response<NodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Create a new Node in a Balancers Config
        /// </summary>
        /// <param name="configId">The Config Id to add the Node to</param>
        /// <param name="label">The Nodes Label</param>
        /// <param name="address">The Nodes Address</param>
        /// <param name="weight">The weight of the Node</param>
        /// <param name="mode">The Nodes Mode</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Create(int configId,
            string label,
            string address,
            int? weight,
            ModeEnum? mode,
            string apiKey,
            Action<Response<NodeResponse>> responseAction)
        {
            if (configId <= 0)
                throw new ArgumentOutOfRangeException("configId");

            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("address");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("ConfigID", configId.ToString());
            req_dict.Add("Label", label);
            req_dict.Add("Address", address);

            if (weight.HasValue)
                req_dict.Add("Weight", weight.Value.ToString());

            if (mode.HasValue)
                req_dict.Add("Mode", mode.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_NODE_CREATE, req_dict);

            var httpClient = new HttpClient<NodeResponse>(req, new Action<Response<NodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Update a Node in a Balancers Config
        /// </summary>
        /// <param name="nodeId">The Node Id to Update</param>
        /// <param name="label">The Nodes Label</param>
        /// <param name="address">The Nodes Address</param>
        /// <param name="weight">The weight of the Node</param>
        /// <param name="mode">The Nodes Mode</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Update(int nodeId,
            string label,
            string address,
            int? weight,
            ModeEnum? mode,
            string apiKey,
            Action<Response<NodeResponse>> responseAction)
        {
            if (nodeId <= 0)
                throw new ArgumentOutOfRangeException("nodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("NodeID", nodeId.ToString());

            if(!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if(!string.IsNullOrEmpty(address))
                req_dict.Add("Address", address);

            if (weight.HasValue)
                req_dict.Add("Weight", weight.Value.ToString());

            if (mode.HasValue)
                req_dict.Add("Mode", mode.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_NODE_UPDATE, req_dict);

            var httpClient = new HttpClient<NodeResponse>(req, new Action<Response<NodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

    }
}
