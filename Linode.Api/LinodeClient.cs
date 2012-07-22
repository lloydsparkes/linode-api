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
    public static class LinodeClient
    {
        #region Login & Check Auth Methods

        /// <summary>
        /// Check to see if a Username & Password Combination is valid and get a API Key
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="responseAction">The Action to Callback to, with the response</param>
        public static void Login(string username, string password, Action<Response<ApiKey>> responseAction)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("password");

            var req_dict = new Dictionary<string,string>();
            req_dict.Add("username", username);
            req_dict.Add("password", password);

            var req = new Request(LinodeActions.USER_GETAPIKEY, req_dict);

            var httpClient = new HttpClient<ApiKey>(req, new Action<Response<ApiKey>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Gets the Account Information - Including Active From and Bandwidth Pool Information
        /// Can Also be used for validating an api key
        /// </summary>
        /// <param name="apiKey">The Users API Key</param>
        /// <param name="responseAction">The Action to Callback to, with the response</param>
        public static void GetAccountInformation(string apiKey, Action<Response<AccountInformation>> responseAction)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey");

            var req = new Request(apiKey, LinodeActions.ACCOUNT_INFO, new Dictionary<string,string>());

            var httpClient = new HttpClient<AccountInformation>(req, new Action<Response<AccountInformation>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #endregion

        #region Linode Actions

        /// <summary>
        /// Get a List of Linodes - Optionally Restricted to a Specific Linode Id
        /// </summary>
        /// <param name="linodeId">Optional - Restrict the list of Linodes to a single Linode</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">To action to callback to with the response</param>
        public static void GetLinodes(int? linodeId, string apiKey, Action<Response<Node[]>> responseAction)
        {
            var req_dict = new Dictionary<string, string>();
            if(linodeId.HasValue && linodeId.Value > 0)
                req_dict.Add("LinodeID", linodeId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_LIST, req_dict);

            var httpClient = new HttpClient<Node[]>(req, new Action<Response<Node[]>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #region Boot, Reboot Shutdown

        /// <summary>
        /// Boot a Linode
        /// </summary>
        /// <param name="linodeId">The Linode ID</param>
        /// <param name="configId">The Configuration ID (optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return to with the response</param>
        public static void BootLinode(int linodeId, int? configId, string apiKey, Action<Response<JobResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (configId.HasValue && configId.Value <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();

            req_dict.Add("LinodeID", linodeId.ToString());

            if (configId.HasValue)
                req_dict.Add("ConfigID", configId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_BOOT, req_dict);

            var httpClient = new HttpClient<JobResponse>(req, new Action<Response<JobResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        /// <summary>
        /// Reboots a Linode
        /// </summary>
        /// <param name="linodeId">The Linode ID</param>
        /// <param name="configId">The Configuration ID (optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return to with the response</param>
        public static void RebootLinode(int linodeId, int? configId, string apiKey, Action<Response<JobResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (configId.HasValue && configId.Value <= 0)
                throw new ArgumentOutOfRangeException("configId");

            var req_dict = new Dictionary<string, string>();

            req_dict.Add("LinodeID", linodeId.ToString());

            if (configId.HasValue)
                req_dict.Add("ConfigID", configId.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_REBOOT, req_dict);

            var httpClient = new HttpClient<JobResponse>(req, new Action<Response<JobResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        /// <summary>
        /// Shutdown a Linode
        /// </summary>
        /// <param name="linodeId">The Linode ID</param>
        /// <param name="configId">The Configuration ID (optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return to with the response</param>
        public static void ShutdownLinode(int linodeId, string apiKey, Action<Response<JobResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();

            req_dict.Add("LinodeID", linodeId.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_SHUTDOWN, req_dict);

            var httpClient = new HttpClient<JobResponse>(req, new Action<Response<JobResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        #endregion

        #region Create, Delete, Update

        /// <summary>
        /// Create a Linode
        /// </summary>
        /// <param name="dataCenterId">The Data Center Id</param>
        /// <param name="planId">The Plan Id</param>
        /// <param name="paymentTerm">The Payment Term</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The Response action to return the response to</param>
        public static void CreateLinode(int dataCenterId, int planId, PaymentTerm paymentTerm,
            string apiKey, Action<Response<LinodeResponse>> responseAction)
        {
            if (dataCenterId <= 0)
                throw new ArgumentOutOfRangeException("dataCenterId");

            if (planId <= 0)
                throw new ArgumentOutOfRangeException("planId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("DatacenterID", dataCenterId.ToString());
            req_dict.Add("PlanID", planId.ToString());
            req_dict.Add("PaymentTerm", ((int)paymentTerm).ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_CREATE, req_dict);

            var httpClient = new HttpClient<LinodeResponse>(req, new Action<Response<LinodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        /// <summary>
        /// Delete a Linode
        /// </summary>
        /// <param name="linodeId">The Id of the Linode to delete</param>
        /// <param name="skipChecks">Skip Safety Checks (optional - defaults to false)</param>
        /// <param name="apiKey">The users Api Key</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void DeleteLinode(int linodeId, bool? skipChecks, string apiKey, Action<Response<LinodeResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeId", linodeId.ToString());

            if (skipChecks.HasValue)
                req_dict.Add("skipChecks", skipChecks.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_DELETE, req_dict);

            var httpClient = new HttpClient<LinodeResponse>(req, new Action<Response<LinodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        /// <summary>
        /// Updates the Alert / Backup Options for a Linode
        /// Also the Label and Linode Manager Display Group
        /// </summary>
        /// <param name="linodeId">The Id of the Linode to Update</param>
        /// <param name="label">The new Label of the Linode (optional)</param>
        /// <param name="lpmDisplayGroup">The Linode Manager Display Group (optional)</param>
        /// <param name="alertCpuEnabled">Should CPU Usage Alerts be Enabled (optional)</param>
        /// <param name="alertDiskEnabled">Should Disk IO Usage Alerts be Enabled (optional)</param>
        /// <param name="alertBWInEnabled">Should Bandwidth In Usage Alerts be Enabled (optional)</param>
        /// <param name="alertBWOutEnabled">Should Bandwidth Out Usage Alerts be Enabled (optional)</param>
        /// <param name="alertBWQuotaEnabled">Should Bandwidth Quota Alerts be Enabled (optional)</param>
        /// <param name="alertCpuThreshold">What should the CPU Alert Threshold be (optional)</param>
        /// <param name="alertDiskThreshold">What should the Disk Alert Threshold be (optional)</param>
        /// <param name="alertBWInThreshold">What should the Bandwidth In Threshold be (optional)</param>
        /// <param name="alertBWOutThreshold">What should the Bandwidth Out Threshold be (optional)</param>
        /// <param name="alertBWQuotaThreshold">What should the Bandwidth Quota Threshold be (optional)</param>
        /// <param name="watchdogEnabled">Should the (Lassie?) Watchdog be Enabled (optional)</param>
        /// <param name="backupWindow">What should the backup Window be (0-23) (optional)</param>
        /// <param name="backupWeeklyDay">What should the backup Day of the Week be (0-7) (optional)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action the response should be given to</param>
        public static void UpdateLinode(
            int linodeId,
            String label,
            String lpmDisplayGroup,
            bool? alertCpuEnabled,
            bool? alertDiskEnabled,
            bool? alertBWInEnabled,
            bool? alertBWOutEnabled,
            bool? alertBWQuotaEnabled,
            int? alertCpuThreshold,
            int? alertDiskThreshold,
            int? alertBWInThreshold,
            int? alertBWOutThreshold,
            int? alertBWQuotaThreshold,
            bool? watchdogEnabled,
            int? backupWindow,
            int? backupWeeklyDay,
            string apiKey,
            Action<Response<LinodeResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if(backupWindow.HasValue && (backupWindow.Value < 0 || backupWindow.Value >23))
                throw new ArgumentOutOfRangeException("backupWindow");

            if (backupWeeklyDay.HasValue && (backupWeeklyDay.Value < 0 || backupWeeklyDay.Value > 7))
                throw new ArgumentOutOfRangeException("backupWeeklyDay");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeId", linodeId.ToString());

            if (!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if (!string.IsNullOrEmpty(lpmDisplayGroup))
                req_dict.Add("lpm_displayGroup", lpmDisplayGroup);

            if (alertCpuEnabled.HasValue)
                req_dict.Add("Alert_cpu_enabled", alertCpuEnabled.Value.ToString().ToLower());

            if (alertDiskEnabled.HasValue)
                req_dict.Add("Alert_diskio_enabled", alertDiskEnabled.Value.ToString().ToLower());

            if (alertBWInEnabled.HasValue)
                req_dict.Add("Alert_bwin_enabled", alertBWInEnabled.Value.ToString().ToLower());

            if (alertBWOutEnabled.HasValue)
                req_dict.Add("Alert_bwout_enabled", alertBWOutEnabled.Value.ToString().ToLower());

            if (alertBWQuotaEnabled.HasValue)
                req_dict.Add("Alert_bwquota_enabled", alertBWQuotaEnabled.Value.ToString().ToLower());

            if (alertCpuThreshold.HasValue)
                req_dict.Add("Alert_cpu_threshold", alertCpuThreshold.Value.ToString());

            if (alertDiskThreshold.HasValue)
                req_dict.Add("Alert_diskio_threshold", alertDiskThreshold.Value.ToString());

            if (alertBWInThreshold.HasValue)
                req_dict.Add("Alert_bwin_threshold", alertBWInThreshold.Value.ToString());

            if (alertBWOutThreshold.HasValue)
                req_dict.Add("Alert_bwout_threshold", alertBWOutThreshold.Value.ToString());

            if (alertBWQuotaThreshold.HasValue)
                req_dict.Add("Alert_bwquota_threshold", alertBWQuotaThreshold.Value.ToString());

            if (watchdogEnabled.HasValue)
                req_dict.Add("watchdog", watchdogEnabled.Value.ToString().ToLower());

            if (backupWindow.HasValue)
                req_dict.Add("backupWindow", alertBWOutThreshold.Value.ToString());

            if (backupWeeklyDay.HasValue)
                req_dict.Add("backupWeeklyDay", alertBWQuotaThreshold.Value.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_UPDATE, req_dict);

            var httpClient = new HttpClient<LinodeResponse>(req, new Action<Response<LinodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }
            
        #endregion

        #region Clone, Resize

        /// <summary>
        /// Clone a Linode
        /// </summary>
        /// <param name="linodeId">The Linode Id</param>
        /// <param name="dataCenterId">The Data Center Id</param>
        /// <param name="planId">The Plan Id</param>
        /// <param name="paymentTerm">The Payment Term</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The Response action to return the response to</param>
        public static void CloneLinode(int linodeId, int dataCenterId, int planId, PaymentTerm paymentTerm, 
            string apiKey, Action<Response<LinodeResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (dataCenterId <= 0)
                throw new ArgumentOutOfRangeException("dataCenterId");

            if (planId <= 0)
                throw new ArgumentOutOfRangeException("planId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DatacenterID", dataCenterId.ToString());
            req_dict.Add("PlanID", planId.ToString());
            req_dict.Add("PaymentTerm", ((int)paymentTerm).ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_CLONE, req_dict);

            var httpClient = new HttpClient<LinodeResponse>(req, new Action<Response<LinodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();

        }

        /// <summary>
        /// Resizes a Linode
        /// </summary>
        /// <param name="linodeId">The Id of the Linode to resize</param>
        /// <param name="planId">The Plan Id to resize to</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void ResizeLinode(int linodeId, int planId, string apiKey, Action<Response<LinodeResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (planId <= 0)
                throw new ArgumentOutOfRangeException("planId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("PlanID", planId.ToString());

            var req = new Request(apiKey, LinodeActions.LINODE_RESIZE, req_dict);

            var httpClient = new HttpClient<LinodeResponse>(req, new Action<Response<LinodeResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #endregion

        #endregion

        #region Linode Disk Actions

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
        public static void DiskCreate(int linodeId, string label, DiskFormatEnum format, bool? isReadOnly, int size, string apiKey, Action<Response<DiskResponse>> responseAction)
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
        public static void DiskCreateFromDistribution(int linodeId, int distributionId, string label, int size, string rootPassword, string rootSSHKey, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if(distributionId <= 0)
                throw new ArgumentOutOfRangeException("distributionId");

            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("size");

            if(string.IsNullOrEmpty(rootPassword))
                throw new ArgumentNullException("rootPassword");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DistributionID", distributionId.ToString());
            req_dict.Add("Label", label);
            req_dict.Add("Size", size.ToString());
            req_dict.Add("rootPass", rootPassword);

            if(!string.IsNullOrEmpty(rootSSHKey))
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
        public static void DiskCreateFromStackScript(int linodeId, int distributionId, int stackScriptId, string stackscriptUDFResponses, string label, int size, string rootPassword, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if(distributionId <= 0)
                throw new ArgumentOutOfRangeException("distributionId");

            if (stackScriptId <= 0)
                throw new ArgumentOutOfRangeException("stackScriptId");

            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            if (null == stackscriptUDFResponses)
                throw new ArgumentNullException("stackscriptUDFResponses");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("size");

            if(string.IsNullOrEmpty(rootPassword))
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
        public static void DiskDelete(int linodeId, int diskId, string apiKey, Action<Response<DiskResponse>> responseAction)
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
        public static void DiskDuplicate(int linodeId, int diskId, string apiKey, Action<Response<DiskResponse>> responseAction)
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
        public static void DiskList(int linodeId, int? diskId, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());

            if(diskId.HasValue)
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
        public static void DiskResize(int linodeId, int diskId, int size, string apiKey, Action<Response<DiskResponse>> responseAction)
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
        public static void DiskUpdate(int linodeId, int diskId, string label, bool? isReadOnly, string apiKey, Action<Response<DiskResponse>> responseAction)
        {
            if (linodeId <= 0)
                throw new ArgumentOutOfRangeException("linodeId");

            if (diskId <= 0)
                throw new ArgumentOutOfRangeException("diskId");

            var req_dict = new Dictionary<string, string>();
            req_dict.Add("LinodeID", linodeId.ToString());
            req_dict.Add("DiskID", diskId.ToString());
            
            if(!string.IsNullOrEmpty(label))
                req_dict.Add("Label", label);

            if(isReadOnly.HasValue)
                req_dict.Add("isReadOnly", isReadOnly.Value.ToString().ToLower());

            var req = new Request(apiKey, LinodeActions.LINODE_DISK_UPDATE, req_dict);

            var httpClient = new HttpClient<DiskResponse>(req, new Action<Response<DiskResponse>>(resp =>
            {
                if (responseAction != null)
                    responseAction.Invoke(resp);
            }));

            httpClient.InvokeGet();
        }

        #endregion

        #region Linode Config Actions

        /// <summary>
        /// Lists the Configurations for a Given Linode
        /// </summary>
        /// <param name="linodeId">The Linode Id to get Configs for</param>
        /// <param name="configId">The Config Id to get (optional)</param>
        /// <param name="apiKey">The users Api Key</param>
        /// <param name="responseAction">The action to send the response to</param>
        public static void ListConfigurations(int linodeId, int? configId, string apiKey, Action<Response<Config[]>> responseAction)
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
        public static void DeleteConfiguration(int linodeId, int configId, string apiKey, Action<Response<ConfigResponse>> responseAction)
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
        public static void CreateConfiguration(int linodeId,
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

            if(diskList != null)
                req_dict.Add("DiskList", diskList.Select(a => a.ToString()).Aggregate((a,b) => a.ToString() + "," + b.ToString()));

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
        public static void UpdateConfiguration(int linodeId,
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

            if(kernelId.HasValue)
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

        #endregion

        #region Linode IP Actions

        /// <summary>
        /// Adds a new Private IP to the Linode
        /// </summary>
        /// <param name="linodeId">The Id of the Linode to add the IP to</param>
        /// <param name="apiKey">The Api Key of the User</param>
        /// <param name="responseAction">The action to return the response to</param>
        public static void AddPrivateIp(int linodeId, string apiKey, Action<Response<IpResponse>> responseAction)
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
        public static void GetIps(int linodeId, int? ipAddressId, string apiKey, Action<Response<Ip[]>> responseAction)
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

        #endregion

        #region Linode Job Actions

        /// <summary>
        /// Gets a List of Jobs for the Given Linode
        /// </summary>
        /// <param name="linodeId">The Linode Id to Get Jobs for</param>
        /// <param name="jobId">The Job Id to limit the results to (optional)</param>
        /// <param name="pendingOnly">Set to true if you only want Pending Jobs (optional - default false)</param>
        /// <param name="apiKey">The Users Api Key</param>
        /// <param name="responseAction">The Action to pass the response to</param>
        public static void GetJobs(int linodeId, int? jobId, bool? pendingOnly, string apiKey, Action<Response<Job[]>> responseAction)
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

        #endregion
    }
}
