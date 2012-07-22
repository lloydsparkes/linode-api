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
    public static class DnsMethods
    {
        /// <summary>
        /// Get a list of Domains
        /// </summary>
        /// <param name="domainId">Optional Domain Id if you want to filter the list</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void List(int? domainId, string apiKey, Action<Response<Domain[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();
            if (domainId.HasValue && domainId.Value > 0)
                req_dict.Add("DomainID", domainId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.DOMAIN_LIST, req_dict);

            var httpClient = new HttpClient<Domain[]>(req, new Action<Response<Domain[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Deletes a Domain
        /// </summary>
        /// <param name="domainId">Domain Id to delete</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Delete(int domainId, string apiKey, Action<Response<DomainResponse>> responseAction)
        {
            if (domainId <= 0)
                throw new ArgumentOutOfRangeException("domainId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DomainID", domainId.ToString());

            var req = new Request(apiKey, LinodeActions.DOMAIN_DELETE, req_dict);

            var httpClient = new HttpClient<DomainResponse>(req, new Action<Response<DomainResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Create a new Domain Record
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <param name="description">Description (Optional)</param>
        /// <param name="type">Type (Master/Slave) (Optional)</param>
        /// <param name="soaEmail">SOA Email (Optional - unless Type=Master)</param>
        /// <param name="refreshSec">Refresh Seconds (Optional)</param>
        /// <param name="retrySec">Retry Seconds (Optional)</param>
        /// <param name="expireSec">Expire Seconds (Optional)</param>
        /// <param name="ttlSec">TTL Seconds (Optional)</param>
        /// <param name="status">Status</param>
        /// <param name="masterIps"></param>
        /// <param name="axfrIps"></param>
        /// <param name="apiKey"></param>
        /// <param name="responseAction"></param>
        public static void Create(
            string domain,
            string description,
            DomainTypeEnum type,
            string soaEmail,
            int? refreshSec,
            int? retrySec,
            int? expireSec,
            int? ttlSec,
            DomainStatusEnum? status,
            string masterIps,
            string axfrIps,
            string apiKey,
            Action<Response<DomainResponse>> responseAction)
        {
            if (string.IsNullOrEmpty(domain))
                throw new ArgumentOutOfRangeException("domain");

            if (type == DomainTypeEnum.Master && string.IsNullOrEmpty(soaEmail))
                throw new ArgumentNullException("soaEmail");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("Domain", domain);
            req_dict.Add("Type", type.ToString().ToLower());
            
            if (!string.IsNullOrEmpty(description))
                req_dict.Add("Description", description);

            if (!string.IsNullOrEmpty(masterIps))
                req_dict.Add("master_ips", masterIps);

            if (!string.IsNullOrEmpty(axfrIps))
                req_dict.Add("axfr_ips", axfrIps);

            if (!string.IsNullOrEmpty(soaEmail))
                req_dict.Add("SOA_Email", soaEmail);       
            
            var req = new Request(apiKey, LinodeActions.DOMAIN_CREATE, req_dict);

            var httpClient = new HttpClient<DomainResponse>(req, new Action<Response<DomainResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Update a Domain Record
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <param name="description">Description (Optional)</param>
        /// <param name="type">Type (Master/Slave) (Optional)</param>
        /// <param name="soaEmail">SOA Email (Optional - unless Type=Master)</param>
        /// <param name="refreshSec">Refresh Seconds (Optional)</param>
        /// <param name="retrySec">Retry Seconds (Optional)</param>
        /// <param name="expireSec">Expire Seconds (Optional)</param>
        /// <param name="ttlSec">TTL Seconds (Optional)</param>
        /// <param name="status">Status</param>
        /// <param name="masterIps"></param>
        /// <param name="axfrIps"></param>
        /// <param name="apiKey"></param>
        /// <param name="responseAction"></param>
        public static void Update(
            int domainId,
            string domain,
            string description,
            DomainTypeEnum? type,
            string soaEmail,
            int? refreshSec,
            int? retrySec,
            int? expireSec,
            int? ttlSec,
            DomainStatusEnum? status,
            string masterIps,
            string axfrIps,
            string apiKey,
            Action<Response<DomainResponse>> responseAction)
        {
            if (domainId <= 0)
                throw new ArgumentOutOfRangeException("domainId");

            if (type.HasValue && type == DomainTypeEnum.Master && string.IsNullOrEmpty(soaEmail))
                throw new ArgumentNullException("soaEmail");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DomainID", domainId.ToString());

            if (!string.IsNullOrEmpty(domain))
                req_dict.Add("Domain", domain);

            if(type.HasValue)
                req_dict.Add("Type", type.ToString().ToLower());

            if (!string.IsNullOrEmpty(description))
                req_dict.Add("Description", description);

            if (!string.IsNullOrEmpty(masterIps))
                req_dict.Add("master_ips", masterIps);

            if (!string.IsNullOrEmpty(axfrIps))
                req_dict.Add("axfr_ips", axfrIps);

            if (!string.IsNullOrEmpty(soaEmail))
                req_dict.Add("SOA_Email", soaEmail);

            var req = new Request(apiKey, LinodeActions.DOMAIN_UPDATE, req_dict);

            var httpClient = new HttpClient<DomainResponse>(req, new Action<Response<DomainResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
    }
}
