using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.ObjectModel.Linode
{
    public enum RunLevelEnum
    {
        Default,
        Single,
        BinBash,
    }

    public class Config
    {
        [JsonProperty("ConfigID")]
        public int Id { get; set; }

        [JsonProperty("LinodeId")]
        public int LinodeId { get; set; }

        [JsonProperty("Label")]
        public string Name { get; set; }

        [JsonProperty("Comments")]
        public string Comment { get; set; }

        [JsonProperty("DiskList")]
        public string DiskListRaw { get; set; }
        public List<int> DiskIds
        {
            get
            {
                var list = new List<int>();

                if (string.IsNullOrEmpty(DiskListRaw))
                    return list;

                var split = DiskListRaw.Split(',');
                foreach (var id in split)
                {
                    int temp = -1;
                    if (int.TryParse(id, out temp))
                    {
                        list.Add(temp);
                    }
                }

                return list;
            }
        }

        [JsonProperty("KernelID")]
        public int KernelId { get; set; }

        [JsonProperty("RAMLimit")]
        public int MemoryLimit { get; set; }

        [JsonProperty("helper_xen")]
        public int XenHelperRaw { get; set; }
        public bool XenHelper { get { return XenHelperRaw == 1; } }

        [JsonProperty("helper_depmod")]
        public int DepModHelperRaw { get; set; }
        public bool DepModHelper { get { return DepModHelperRaw == 1; } }

        [JsonProperty("helper_libtls")]
        public int LibTLSHelperRaw { get; set; }
        public bool LibTLSHelper { get { return LibTLSHelperRaw == 1; } }

        [JsonProperty("helper_disableUpdateDB")]
        public int DisableUpdateDBHelperRaw { get; set; }
        public bool DisableUpdateDBHelper { get { return DisableUpdateDBHelperRaw == 1; } }

        [JsonProperty("RootDeviceRO")]
        public bool RootDeviceReadOnly { get; set; }

        [JsonProperty("RootDeviceCustom")]
        public string CustomRootDevice { get; set; }

        [JsonProperty("")]
        public string RunLevelRaw { get; set; }
        public RunLevelEnum RunLevel
        {
            get
            {
                if (string.IsNullOrEmpty(RunLevelRaw))
                    return RunLevelEnum.Default;

                try
                {
                    return (RunLevelEnum)Enum.Parse(typeof(RunLevelEnum), RunLevelRaw, true);
                }
                catch (Exception e)
                {
                    return RunLevelEnum.Default;
                }
            }
        }
    }
}
