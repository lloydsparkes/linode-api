using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linode.Api.Base;
using Linode.Api.Reference;
using Linode.Api.Utility;


namespace Linode.Api
{
    public static class ScriptMethods
    {
        /// <summary>
        /// Lists The Stacks Scrips 
        /// </summary>
        /// <param name="stackScriptId"></param>
        /// <param name="apiKey"></param>
        /// <param name="responseAction"></param>
        public static void List(int? stackScriptId, string apiKey, Action<Response<Script[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();

            if (stackScriptId.HasValue && stackScriptId.Value > 0)
                req_dict.Add("StackScriptID", stackScriptId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.STACKSCRIPT_LIST, req_dict);

            var httpClient = new HttpClient<Script[]>(req, new Action<Response<Script[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Delete a StackScript
        /// </summary>
        /// <param name="stackScriptId">The StackScript to delete</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void Delete(int stackScriptId, string apiKey, Action<Response<StackScriptResponse>> responseAction)
        {
            if (stackScriptId <= 0)
                throw new ArgumentOutOfRangeException("stackScriptId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("StackScriptID", stackScriptId.ToString());

            var req = new Request(apiKey, LinodeActions.STACKSCRIPT_DELETE, req_dict);

            var httpClient = new HttpClient<StackScriptResponse>(req, new Action<Response<StackScriptResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Updates a StackScript
        /// </summary>
        /// <param name="stackScriptId">The stackscript id</param>
        /// <param name="label">The label for the script (Optional)</param>
        /// <param name="description">Description (Optional)</param>
        /// <param name="distributionIdList">List of Distributions this Script is valid for (Optional)</param>
        /// <param name="isPublic">Is this a public stack script (Optional)</param>
        /// <param name="revNote">A message related to this revision (Optional)</param>
        /// <param name="script">The script (Optional)</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Update(int stackScriptId,
            string label,
            string description,
            List<int> distributionIdList,
            bool? isPublic,
            string revNote,
            string script,
            string apiKey, Action<Response<StackScriptResponse>> responseAction)
        {
            if (stackScriptId <= 0)
                throw new ArgumentOutOfRangeException("stackScriptId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("StackScriptID", stackScriptId.ToString());

            if (!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if (!string.IsNullOrEmpty(description))
                req_dict.Add("Description", description);

            if (!string.IsNullOrEmpty(revNote))
                req_dict.Add("rev_note", revNote);

            if (!string.IsNullOrEmpty(script))
                req_dict.Add("script", script);

            if (isPublic.HasValue)
                req_dict.Add("isPublic", isPublic.Value.ToString().ToLower());

            if(distributionIdList != null)
                req_dict.Add("DistributionIDList", distributionIdList.Select(a => a.ToString()).Aggregate((a,b) => a + "," + b));


            var req = new Request(apiKey, LinodeActions.STACKSCRIPT_DELETE, req_dict);

            var httpClient = new HttpClient<StackScriptResponse>(req, new Action<Response<StackScriptResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Create a new StackScript
        /// </summary>
        /// <param name="label">The label for the script</param>
        /// <param name="description">Description</param>
        /// <param name="distributionIdList">List of Distributions this Script is valid for</param>
        /// <param name="isPublic">Is this a public stack script</param>
        /// <param name="revNote">A message related to this revision</param>
        /// <param name="script">The script</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Update(
            string label,
            string description,
            List<int> distributionIdList,
            bool? isPublic,
            string revNote,
            string script,
            string apiKey, Action<Response<StackScriptResponse>> responseAction)
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            if (distributionIdList == null)
                throw new ArgumentNullException("distributionIdList");

            if (string.IsNullOrEmpty(script))
                throw new ArgumentNullException("script");

            var req_dict = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if (!string.IsNullOrEmpty(description))
                req_dict.Add("Description", description);

            if (!string.IsNullOrEmpty(revNote))
                req_dict.Add("rev_note", revNote);

            if (!string.IsNullOrEmpty(script))
                req_dict.Add("script", script);

            if (isPublic.HasValue)
                req_dict.Add("isPublic", isPublic.Value.ToString().ToLower());

            if (distributionIdList != null)
                req_dict.Add("DistributionIDList", distributionIdList.Select(a => a.ToString()).Aggregate((a, b) => a + "," + b));


            var req = new Request(apiKey, LinodeActions.STACKSCRIPT_DELETE, req_dict);

            var httpClient = new HttpClient<StackScriptResponse>(req, new Action<Response<StackScriptResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

    }
}
