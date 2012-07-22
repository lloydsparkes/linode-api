using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linode.Api.Base;
using Linode.Api.Linode;
using Linode.Api.Reference;
using Linode.Api.Utility;

namespace Linode.Api.Api
{
    public static class NodeJobMethods
    {
        /// <summary>
        /// Gets a List of Jobs for the Given Linode
        /// </summary>
        /// <param name="linodeId">The Linode Id to Get Jobs for</param>
        /// <param name="jobId">The Job Id to limit the results to (optional)</param>
        /// <param name="pendingOnly">Set to true if you only want Pending Jobs (optional - default false)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The Action to pass the response to</param>
        public static void List(int linodeId, int? jobId, bool? pendingOnly, string apiKey, Action<Response<Job[]>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());

            if (jobId.HasValue && jobId.Value > 0)
                req_dict.Add("JobID", jobId.Value.ToString());

            if (pendingOnly.HasValue)
                req_dict.Add("pendingOnly", pendingOnly.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.LINODE_JOB_LIST, req_dict);

            var httpClient = new HttpClient<Job[]>(req, new Action<Response<Job[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

    }
}
