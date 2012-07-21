using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Linode
{
    public class Node
    {
        [JsonProperty("LINODEID")]
        public String Id { get; set; }
        
        [JsonProperty("LABEL")]
        public String Label { get; set; }

        [JsonProperty("STATUS")]
        public int Status { get; set; }

        [JsonProperty("LPM_DISPLAYGROUP")]
        public String LpmDisplayGroup { get; set; }

        [JsonProperty("TOTALRAM")]
        public int MemorySize { get; set; }

        [JsonProperty("TOTALHD")]
        public int DiskSpace { get; set; }

        [JsonProperty("TOTALXFER")]
        public int TotalXfer { get; set; }

        [JsonProperty("WATCHDOG")]
        public int WatchdogEnabledRaw { get; set; }
        public bool WatchdogEnabled { get { return WatchdogEnabledRaw == 1; } }

        #region Backup Settings

        [JsonProperty("BACKUPSENABLED")]
        public int BackupEnabledRaw { get; set; }
        public bool BackupEnabled { get { return BackupEnabledRaw == 1; } }

        [JsonProperty("BACKUPWINDOW")]
        public int BackupWindow { get; set; }

        [JsonProperty("BACKUPWEEKLYDAY")]
        public int BackupDayOfWeek { get; set; }

        #endregion

        #region Alert Settings

        [JsonProperty("ALERT_BWQUOTA_ENABLED")]
        public int AlertOnBandwidthQuotaRaw { get; set; }
        public bool AlertOnBandwidthQuota { get { return AlertOnBandwidthQuotaRaw == 1; } }

        [JsonProperty("ALERT_BWQUOTA_THRESHOLD")]
        public int AlertBandwidthQuotaThreshold { get; set; }

        [JsonProperty("ALERT_BWOUT_ENABLED")]
        public int AlertOnBandwidthOutRaw { get; set; }
        public bool AlertOnBandwidthOut { get { return AlertOnBandwidthOutRaw == 1; } }

        [JsonProperty("ALERT_BWOUT_THRESHOLD")]
        public int AlertBandwidthOutThreshold { get; set; }

        [JsonProperty("ALERT_BWIN_ENABLED")]
        public int AlertOnBandwidthInRaw { get; set; }
        public bool AlertOnBandwidthIn { get { return AlertOnBandwidthInRaw == 1; } }

        [JsonProperty("ALERT_BWIN_THRESHOLD")]
        public int AlertBandwidthInThreshold { get; set; }

        [JsonProperty("ALERT_DISKIO_ENABLED")]
        public int AlertOnDiskIORaw { get; set; }
        public bool AlertOnDiskIO { get { return AlertOnDiskIORaw == 1; } }

        [JsonProperty("ALERT_DISKIO_THRESHOLD")]
        public int AlertOnDiskIOThreshold { get; set; }

        [JsonProperty("ALERT_CPU_ENABLED")]
        public int AlertOnCpuUsageRaw { get; set; }
        public bool AlertOnCpuUsage { get { return AlertOnCpuUsageRaw == 1; } }

        [JsonProperty("ALERT_CPU_THRESHOLD")]
        public int AlertCpuThreashold { get; set; }

        #endregion
    }
}
