using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linode.Api.Base;
using Linode.Api.Linode;
using Linode.Api.Balancer;
using Linode.Api.Utility;

namespace Linode.Api.Api
{
    public class BalancerConfigMethods
    {
        /// <summary>
        /// List all the Configs for the Given Node Balancer
        /// </summary>
        /// <param name="nodeBalancerId">The Node Balancer ID</param>
        /// <param name="configId">Optional Config ID</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void List(int nodeBalancerId, int? configId, string apiKey, Action<Response<BalancerConfig[]>> responseAction)
        {
            if (nodeBalancerId <= 0)
                throw new ArgumentOutOfRangeException("nodeBalancerId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("NodeBalancerID", nodeBalancerId.ToString());

            if (configId.HasValue && configId.Value > 0)
                req_dict.Add("ConfigID", configId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_CONFIG_LIST, req_dict);

            var httpClient = new HttpClient<BalancerConfig[]>(req, new Action<Response<BalancerConfig[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Delete a Balancer Config
        /// </summary>
        /// <param name="configId">The ID of the Config To Delete</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Delete(int configId, string apiKey, Action<Response<ConfigResponse>> responseAction)
        {
            if (configId <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("ConfigID", configId.ToString());

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_CONFIG_DELETE, req_dict);

            var httpClient = new HttpClient<ConfigResponse>(req, new Action<Response<ConfigResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Update a Config for a Node Balancer
        /// </summary>
        /// <param name="configId">Config to Update</param>
        /// <param name="port">Port</param>
        /// <param name="protocol">Protocol</param>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="stickiness">Stickiness</param>
        /// <param name="check">Check</param>
        /// <param name="checkInterval">Check Interval</param>
        /// <param name="checkTimeout">Check Timeout</param>
        /// <param name="checkAttempts">Check Attempts</param>
        /// <param name="checkPath">Check Path</param>
        /// <param name="checkBody">Check Body</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Update(
            int configId,
            int? port,
            ProtocolEnum? protocol,
            AlgorithmEnum? algorithm,
            StickinessEnum? stickiness,
            CheckEnum? check,
            int? checkInterval,
            int? checkTimeout,
            int? checkAttempts,
            string checkPath,
            string checkBody,
            string apiKey,
            Action<Response<ConfigResponse>> responseAction)
        {
            if (configId <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("ConfigID", configId.ToString());

            if (port.HasValue)
                req_dict.Add("Port", port.Value.ToString());

            if (protocol.HasValue)
                req_dict.Add("Protocol", protocol.Value.ToString().ToLower());

            if (algorithm.HasValue)
                req_dict.Add("Algorithm", algorithm.Value.ToString().ToLower());

            if (stickiness.HasValue)
                req_dict.Add("Stickiness", stickiness.Value.ToString().ToLower());

            if (check.HasValue)
                req_dict.Add("check", check.Value.ToString().ToLower());

            if (checkInterval.HasValue)
                req_dict.Add("check_interval", checkInterval.Value.ToString());

            if (checkTimeout.HasValue)
                req_dict.Add("check_timeout", checkTimeout.Value.ToString());

            if (checkAttempts.HasValue)
                req_dict.Add("check_attempts", checkAttempts.Value.ToString());

            if (!string.IsNullOrEmpty(checkPath))
                req_dict.Add("check_path", checkPath);

            if (!string.IsNullOrEmpty(checkBody))
                req_dict.Add("check_body", checkBody);

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_CONFIG_UPDATE, req_dict);

            var httpClient = new HttpClient<ConfigResponse>(req, new Action<Response<ConfigResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Create a new Config for a Node Balancer
        /// </summary>
        /// <param name="nodeBalancerId">NodeBalancer to add to</param>
        /// <param name="port">Port</param>
        /// <param name="protocol">Protocol</param>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="stickiness">Stickiness</param>
        /// <param name="check">Check</param>
        /// <param name="checkInterval">Check Interval</param>
        /// <param name="checkTimeout">Check Timeout</param>
        /// <param name="checkAttempts">Check Attempts</param>
        /// <param name="checkPath">Check Path</param>
        /// <param name="checkBody">Check Body</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Create(
            int nodeBalancerId,
            int? port,
            ProtocolEnum? protocol,
            AlgorithmEnum? algorithm,
            StickinessEnum? stickiness,
            CheckEnum? check,
            int? checkInterval,
            int? checkTimeout,
            int? checkAttempts,
            string checkPath,
            string checkBody,
            string apiKey,
            Action<Response<ConfigResponse>> responseAction)
        {
            if (nodeBalancerId <= 0)
                throw new ArgumentOutOfRangeException("nodeBalancerId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("NodeBalancerID", nodeBalancerId.ToString());

            if (port.HasValue)
                req_dict.Add("Port", port.Value.ToString());

            if (protocol.HasValue)
                req_dict.Add("Protocol", protocol.Value.ToString().ToLower());

            if (algorithm.HasValue)
                req_dict.Add("Algorithm", algorithm.Value.ToString().ToLower());

            if (stickiness.HasValue)
                req_dict.Add("Stickiness", stickiness.Value.ToString().ToLower());

            if (check.HasValue)
                req_dict.Add("check", check.Value.ToString().ToLower());

            if (checkInterval.HasValue)
                req_dict.Add("check_interval", checkInterval.Value.ToString());

            if (checkTimeout.HasValue)
                req_dict.Add("check_timeout", checkTimeout.Value.ToString());

            if (checkAttempts.HasValue)
                req_dict.Add("check_attempts", checkAttempts.Value.ToString());

            if (!string.IsNullOrEmpty(checkPath))
                req_dict.Add("check_path", checkPath);

            if (!string.IsNullOrEmpty(checkBody))
                req_dict.Add("check_body", checkBody);

            var req = new Request(apiKey, LinodeActions.NODEBALANCER_CONFIG_CREATE, req_dict);

            var httpClient = new HttpClient<ConfigResponse>(req, new Action<Response<ConfigResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
    }
}
