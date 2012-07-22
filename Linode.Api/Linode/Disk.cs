using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Linode
{
    public enum DiskFormatEnum
    {
        Ext3,
        Swap,
        Raws,
        NullRecord
    }

    public class Disk
    {
        [JsonProperty("DISKID")]
        public int Id { get; set; }

        [JsonProperty("LINODEID")]
        public int LinodeId { get; set; }

        [JsonProperty("LABEL")]
        public string Name { get; set; }

        [JsonProperty("TYPE")]
        public String FormatRaw { get; set; }
        public DiskFormatEnum Format
        {
            get
            {
                if (string.IsNullOrEmpty(FormatRaw))
                    return DiskFormatEnum.NullRecord;

                try
                {
                    return (DiskFormatEnum)Enum.Parse(typeof(DiskFormatEnum), FormatRaw, true);
                }
                catch (Exception e)
                {
                    return DiskFormatEnum.NullRecord;
                }
            }
        }

        [JsonProperty("CREATED_DT")]
        public DateTime Created { get; set; }

        [JsonProperty("UPDATE_DT")]
        public DateTime Updated { get; set; }

        [JsonProperty("ISREADONLY")]
        public int IsReadOnlyRaw { get; set; }
        public bool IsReadOnly { get { return IsReadOnlyRaw == 1; } }

        [JsonProperty("STATUS")]
        public int Status { get; set; }

        [JsonProperty("SIZE")]
        public long Size { get; set; }
    }
}
