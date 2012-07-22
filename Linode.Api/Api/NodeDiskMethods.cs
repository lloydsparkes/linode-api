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
    public static class NodeDiskMethods
    {
        /// <summary>
        /// Create a new Disk
        /// </summary>
        /// <param name="linodeId">The Linode To Create the Disk On</param>
        /// <param name="label">The Label for the Disk</param>
        /// <param name="format">The Format of the Disk</param>
        /// <param name="isReadOnly">Should the disk be readonly (optional)</param>
        /// <param name="size">The Size of the Disk</param>
        /// <param name="apiKey">The Api Key for the User</param>
        /// <param name="responseAction">The action for the response to go to</param>
        public static void Create(int linodeId, string label, DiskFormatEnum format, bool? isReadOnly, int size, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("size");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("Label", label);
            req_dict.Add("Type", format.ToString().ToLower());
            req_dict.Add("Size", size.ToString());

            if (isReadOnly.HasValue)
                req_dict.Add("isReadOnly", isReadOnly.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_CREATE, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Creates a new disk using a distribution
        /// </summary>
        /// <param name="linodeId">The linode to create the disk on</param>
        /// <param name="distributionId">The distribution id to use</param>
        /// <param name="label">The label on the disk</param>
        /// <param name="size">The size of the disk</param>
        /// <param name="rootPassword">The root password</param>
        /// <param name="rootSSHKey">The root ssh key (optional)</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void CreateFromDistribution(int linodeId, int distributionId, string label, int size, string rootPassword, string rootSSHKey, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (distributionId <= 0)
                throw new ArgumentOutOfRangeException("distributionId");

            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("size");

            if (string.IsNullOrEmpty(rootPassword))
                throw new ArgumentNullException("rootPassword");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DistributionID", distributionId.ToString());
            req_dict.Add("Label", label);
            req_dict.Add("Size", size.ToString());
            req_dict.Add("rootPass", rootPassword);

            if (!string.IsNullOrEmpty(rootSSHKey))
                req_dict.Add("rootSSHKey", rootSSHKey);

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_CREATEFROMDISTRIBUTION, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Create a new disk from a Stack Script
        /// </summary>
        /// <param name="linodeId">The Linode Id to create the disk on</param>
        /// <param name="distributionId">The Distribution Id to use</param>
        /// <param name="stackScriptId">The stack script id to use</param>
        /// <param name="stackscriptUDFResponses">Any and All Required </param>
        /// <param name="label">The Label for the disk</param>
        /// <param name="size">The size of the disk</param>
        /// <param name="rootPassword">The root password</param>
        /// <param name="apiKey">The users Api Key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void CreateFromStackScript(int linodeId, int distributionId, int stackScriptId, string stackscriptUDFResponses, string label, int size, string rootPassword, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (distributionId <= 0)
                throw new ArgumentOutOfRangeException("distributionId");

            if (stackScriptId <= 0)
                throw new ArgumentOutOfRangeException("stackScriptId");

            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            if (null == stackscriptUDFResponses)
                throw new ArgumentNullException("stackscriptUDFResponses");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("size");

            if (string.IsNullOrEmpty(rootPassword))
                throw new ArgumentNullException("rootPassword");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DistributionID", distributionId.ToString());
            req_dict.Add("StackScriptID", stackScriptId.ToString());
            req_dict.Add("StackScriptUDFResponses", stackscriptUDFResponses);
            req_dict.Add("Label", label);
            req_dict.Add("Size", size.ToString());
            req_dict.Add("rootPass", rootPassword);

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_CREATEFROMSTACKSCRIPT, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Deletes a Disk
        /// </summary>
        /// <param name="linodeId">The Linode Id the disk is in</param>
        /// <param name="diskId">The Disk Id</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Delete(int linodeId, int diskId, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (diskId <= 0)
                throw new ArgumentOutOfRangeException("diskId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DiskID", diskId.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_DELETE, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Duplicates a Disk
        /// </summary>
        /// <param name="linodeId">The Linode Id the disk is in</param>
        /// <param name="diskId">The Disk Id</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Duplicate(int linodeId, int diskId, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (diskId <= 0)
                throw new ArgumentOutOfRangeException("diskId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DiskID", diskId.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_DUPLICATE, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Lists all of the Disks
        /// </summary>
        /// <param name="linodeId">The Linode Id the disk is in</param>
        /// <param name="diskId">The Disk Id (Optional)</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void List(int linodeId, int? diskId, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());

            if (diskId.HasValue)
                req_dict.Add("DiskID", diskId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_LIST, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Resize a Disk
        /// </summary>
        /// <param name="linodeId">The Linode Id the disk is in</param>
        /// <param name="diskId">The Disk Id</param>
        /// <param name="size">The New size of the Disk</param>
        /// <param name="apiKey">The users api key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void Resize(int linodeId, int diskId, int size, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (diskId <= 0)
                throw new ArgumentOutOfRangeException("diskId");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("size");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DiskID", diskId.ToString());
            req_dict.Add("size", size.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_RESIZE, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Updates a Disk
        /// </summary>
        /// <param name="linodeId">The Linodes Id</param>
        /// <param name="diskId">The Disks Id</param>
        /// <param name="label">The new Label (Optional)</param>
        /// <param name="isReadOnly">Should the image now be read only</param>
        /// <param name="apiKey">The users Api Key</param>
        /// <param name="responseAction">The action the response should be sent to</param>
        public static void Update(int linodeId, int diskId, string label, bool? isReadOnly, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (diskId <= 0)
                throw new ArgumentOutOfRangeException("diskId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DiskID", diskId.ToString());

            if (!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if (isReadOnly.HasValue)
                req_dict.Add("isReadOnly", isReadOnly.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_UPDATE, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
    }
}
