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
    public static class NodeConfigMethods
    {
        /// <summary>
        /// Lists the Configurations for a Given Linode
        /// </summary>
        /// <param name="linodeId">The Linode Id to get Configs for</param>
        /// <param name="configId">The Config Id to get (optional)</param>
        /// <param name="apiKey">The users Api Key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void List(int linodeId, int? configId, string apiKey, Action<Response<Config[]>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());

            if (configId.HasValue && configId.Value > 0)
                req_dict.Add("ConfigID", configId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_CONFIG_LIST, req_dict);

            var httpClient = new HttpClient<Config[]>(req, new Action<Response<Config[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Deletes a Configuration
        /// </summary>
        /// <param name="linodeId">The Linode Id of the Config To Delete</param>
        /// <param name="configId">The Config Id of the Config To Delete</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Delete(int linodeId, int configId, string apiKey, Action<Response<ConfigResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (configId <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("ConfigID", configId.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_CONFIG_DELETE, req_dict);

            var httpClient = new HttpClient<ConfigResponse>(req, new Action<Response<ConfigResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Create a new Configuration
        /// </summary>
        /// <param name="linodeId">The Linode's Id</param>
        /// <param name="kernelId">The New Kernel Id</param>
        /// <param name="label">The new Label (Optional)</param>
        /// <param name="comments">The new comments (Optional)</param>
        /// <param name="ramLimit">The Ram Limit (Optional)</param>
        /// <param name="diskList">The List of Disks (Optional)</param>
        /// <param name="runLevel">The Run Level (Optional)</param>
        /// <param name="rootDeviceNum">The Root Device Number (Optional)</param>
        /// <param name="customRootDevice">Custom Root Device (Optional)</param>
        /// <param name="rootDeviceRO">Should the Root Device be RO? (Optional)</param>
        /// <param name="helperDisableUpdateDb">Disable Helper_UpdateDB (Optional)</param>
        /// <param name="helperXen">Disable Helper_Xen (Optional)</param>
        /// <param name="helperDepMod">Disable Helper_DepMod (Optional)</param>
        /// <param name="devTmpFsAutoMount">Should p_vops mount devtmpfs at Book? (Optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void Create(int linodeId,
            int kernelId,
            string label,
            string comments,
            int? ramLimit,
            List<int> diskList,
            RunLevelEnum? runLevel,
            int? rootDeviceNum,
            string customRootDevice,
            bool? rootDeviceRO,
            bool? helperDisableUpdateDb,
            bool? helperXen,
            bool? helperDepMod,
            bool? devTmpFsAutoMount,
            string apiKey,
            Action<Response<ConfigResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (kernelId <= 0)
                throw new ArgumentOutOfRangeException("kernelId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("KernelID", kernelId.ToString());

            if (!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if (!string.IsNullOrEmpty(comments))
                req_dict.Add("Comments", comments);

            if (ramLimit.HasValue && ramLimit.Value > 0)
                req_dict.Add("RAMLimit", ramLimit.ToString());

            if (diskList != null)
                req_dict.Add("DiskList", diskList.Select(a => a.ToString()).Aggregate((a, b) => a.ToString() + "," + b.ToString()));

            if (runLevel.HasValue)
                req_dict.Add("RunLevel", runLevel.Value.ToString().ToLower());

            if (rootDeviceNum.HasValue)
                req_dict.Add("RootDeviceNum", rootDeviceNum.Value.ToString());

            if (!string.IsNullOrEmpty(customRootDevice))
                req_dict.Add("RootDeviceCustom", customRootDevice);

            if (rootDeviceRO.HasValue)
                req_dict.Add("RootDeviceRO", rootDeviceRO.Value.ToString().ToLower());

            if (helperDisableUpdateDb.HasValue)
                req_dict.Add("helper_disableUpdateDB", helperDisableUpdateDb.Value.ToString().ToLower());

            if (helperXen.HasValue)
                req_dict.Add("helper_xen", helperXen.Value.ToString().ToLower());

            if (helperDepMod.HasValue)
                req_dict.Add("helper_depmod", helperDepMod.Value.ToString().ToLower());

            if (devTmpFsAutoMount.HasValue)
                req_dict.Add("devtmpfs_automount", devTmpFsAutoMount.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.LINODE_CONFIG_CREATE, req_dict);

            var httpClient = new HttpClient<ConfigResponse>(req, new Action<Response<ConfigResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Update the Given Configuration
        /// </summary>
        /// <param name="linodeId">The Linode's Id</param>
        /// <param name="configId">The Config's Id</param>
        /// <param name="kernelId">The New Kernel Id (Optional)</param>
        /// <param name="label">The new Label (Optional)</param>
        /// <param name="comments">The new comments (Optional)</param>
        /// <param name="ramLimit">The Ram Limit (Optional)</param>
        /// <param name="diskList">The List of Disks (Optional)</param>
        /// <param name="runLevel">The Run Level (Optional)</param>
        /// <param name="rootDeviceNum">The Root Device Number (Optional)</param>
        /// <param name="customRootDevice">Custom Root Device (Optional)</param>
        /// <param name="rootDeviceRO">Should the Root Device be RO? (Optional)</param>
        /// <param name="helperDisableUpdateDb">Disable Helper_UpdateDB (Optional)</param>
        /// <param name="helperXen">Disable Helper_Xen (Optional)</param>
        /// <param name="helperDepMod">Disable Helper_DepMod (Optional)</param>
        /// <param name="devTmpFsAutoMount">Should p_vops mount devtmpfs at Book? (Optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void Update(int linodeId,
            int configId,
            int? kernelId,
            string label,
            string comments,
            int? ramLimit,
            List<int> diskList,
            RunLevelEnum? runLevel,
            int? rootDeviceNum,
            string customRootDevice,
            bool? rootDeviceRO,
            bool? helperDisableUpdateDb,
            bool? helperXen,
            bool? helperDepMod,
            bool? devTmpFsAutoMount,
            string apiKey,
            Action<Response<ConfigResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (configId <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("ConfigID", configId.ToString());

            if (kernelId.HasValue)
                req_dict.Add("KernelID", kernelId.Value.ToString());

            if (!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if (!string.IsNullOrEmpty(comments))
                req_dict.Add("Comments", comments);

            if (ramLimit.HasValue && ramLimit.Value > 0)
                req_dict.Add("RAMLimit", ramLimit.ToString());

            if (diskList != null)
                req_dict.Add("DiskList", diskList.Select(a => a.ToString()).Aggregate((a, b) => a.ToString() + "," + b.ToString()));

            if (runLevel.HasValue)
                req_dict.Add("RunLevel", runLevel.Value.ToString().ToLower());

            if (rootDeviceNum.HasValue)
                req_dict.Add("RootDeviceNum", rootDeviceNum.Value.ToString());

            if (!string.IsNullOrEmpty(customRootDevice))
                req_dict.Add("RootDeviceCustom", customRootDevice);

            if (rootDeviceRO.HasValue)
                req_dict.Add("RootDeviceRO", rootDeviceRO.Value.ToString().ToLower());

            if (helperDisableUpdateDb.HasValue)
                req_dict.Add("helper_disableUpdateDB", helperDisableUpdateDb.Value.ToString().ToLower());

            if (helperXen.HasValue)
                req_dict.Add("helper_xen", helperXen.Value.ToString().ToLower());

            if (helperDepMod.HasValue)
                req_dict.Add("helper_depmod", helperDepMod.Value.ToString().ToLower());

            if (devTmpFsAutoMount.HasValue)
                req_dict.Add("devtmpfs_automount", devTmpFsAutoMount.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.LINODE_CONFIG_CREATE, req_dict);

            var httpClient = new HttpClient<ConfigResponse>(req, new Action<Response<ConfigResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
    }
}
