using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linode.Api.Base;
using Linode.Api.Dns;
using Linode.Api.Reference;
using Linode.Api.Utility;

namespace Linode.Api
{
    public static class DnsResourceMethods
    {
        /// <summary>
        /// Get a list of Domains Resources
        /// </summary>
        /// <param name="domainId">Domain Id To Get Resources for</param>
        /// <param name="resourceId">Optional Resource Id to filter the list with</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void List(int domainId, int? resourceId, string apiKey, Action<Response<Resource[]>> responseAction)
        {
            if (domainId <= 0)
                throw new ArgumentOutOfRangeException("domainId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DomainID", domainId.ToString());

            if (resourceId.HasValue && resourceId.Value > 0)
                req_dict.Add("ResourceID", resourceId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.DOMAIN_RESOURCE_LIST, req_dict);

            var httpClient = new HttpClient<Resource[]>(req, new Action<Response<Resource[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Deletes a domain Resource
        /// </summary>
        /// <param name="domainId">The Domain Id</param>
        /// <param name="resourceId">The Resource Id</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void Delete(int domainId, int resourceId, string apiKey, Action<Response<DomainResourceResponse>> responseAction)
        {
            if (domainId <= 0)
                throw new ArgumentOutOfRangeException("domainId");

            if (resourceId <= 0)
                throw new ArgumentOutOfRangeException("resourceId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DomainID", domainId.ToString());
            req_dict.Add("ResourceID", resourceId.ToString());

            var req = new Request(apiKey, LinodeActions.DOMAIN_RESOURCE_DELETE, req_dict);

            var httpClient = new HttpClient<DomainResourceResponse>(req, new Action<Response<DomainResourceResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Creates a new Domain Resource
        /// </summary>
        /// <param name="domainId">The Domain Id</param>
        /// <param name="type">Type of the Resource</param>
        /// <param name="name">The hostname or FQDN. for Type=MX the Subdomain (Optional)</param>
        /// <param name="target">The Target Hostname / IP (Optional)</param>
        /// <param name="priority">The priority for MX and SRV records (Optional)</param>
        /// <param name="weight">The Weight (Optional)</param>
        /// <param name="port">The Port (Optional)</param>
        /// <param name="protocol">The Protocol - SRV Records only (Optional)</param>
        /// <param name="ttlSec">The TTL Seconds (Optional)</param>
        /// <param name="apiKey"></param>
        /// <param name="responseAction"></param>
        public static void Create(int domainId,
            ResourceTypeEnum type,
            string name,
            string target,
            int? priority,
            int? weight,
            int? port,
            string protocol,
            int? ttlSec,
            string apiKey,
            Action<Response<DomainResourceResponse>> responseAction)
        {
            if (domainId <= 0)
                throw new ArgumentOutOfRangeException("domainId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DomainID", domainId.ToString());
            req_dict.Add("Type", type.ToString().ToLower());

            if (!string.IsNullOrEmpty(name))
                req_dict.Add("Name", name);

            if (!string.IsNullOrEmpty(target))
                req_dict.Add("Target", target);

            if (priority.HasValue)
                req_dict.Add("Priority", priority.Value.ToString());

            if (weight.HasValue)
                req_dict.Add("Weight", weight.Value.ToString());

            if (!string.IsNullOrEmpty(protocol))
                req_dict.Add("Protocol", protocol);

            if (ttlSec.HasValue)
                req_dict.Add("TTL_sec", ttlSec.Value.ToString());

            var req = new Request(apiKey, LinodeActions.DOMAIN_RESOURCE_CREATE, req_dict);

            var httpClient = new HttpClient<DomainResourceResponse>(req, new Action<Response<DomainResourceResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Update a Domain Resource
        /// </summary>
        /// <param name="domainId">The Domain Id</param>
        /// <param name="resourceId">The Resource Id</param>
        /// <param name="name">The hostname or FQDN. for Type=MX the Subdomain (Optional)</param>
        /// <param name="target">The Target Hostname / IP (Optional)</param>
        /// <param name="priority">The priority for MX and SRV records (Optional)</param>
        /// <param name="weight">The Weight (Optional)</param>
        /// <param name="port">The Port (Optional)</param>
        /// <param name="protocol">The Protocol - SRV Records only (Optional)</param>
        /// <param name="ttlSec">The TTL Seconds (Optional)</param>
        /// <param name="apiKey"></param>
        /// <param name="responseAction"></param>
        public static void Update(int domainId,
            int resourceId,
            string name,
            string target,
            int? priority,
            int? weight,
            int? port,
            string protocol,
            int? ttlSec,
            string apiKey,
            Action<Response<DomainResourceResponse>> responseAction)
        {
            if (domainId <= 0)
                throw new ArgumentOutOfRangeException("domainId");

            if (resourceId <= 0)
                throw new ArgumentOutOfRangeException("resourceId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DomainID", domainId.ToString());
            req_dict.Add("ResourceID", resourceId.ToString());

            if (!string.IsNullOrEmpty(name))
                req_dict.Add("Name", name);

            if (!string.IsNullOrEmpty(target))
                req_dict.Add("Target", target);

            if (priority.HasValue)
                req_dict.Add("Priority", priority.Value.ToString());

            if (weight.HasValue)
                req_dict.Add("Weight", weight.Value.ToString());

            if (!string.IsNullOrEmpty(protocol))
                req_dict.Add("Protocol", protocol);

            if (ttlSec.HasValue)
                req_dict.Add("TTL_sec", ttlSec.Value.ToString());

            var req = new Request(apiKey, LinodeActions.DOMAIN_RESOURCE_UPDATE, req_dict);

            var httpClient = new HttpClient<DomainResourceResponse>(req, new Action<Response<DomainResourceResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
    }
}
