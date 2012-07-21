using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Reference
{
    public class AccountInformation
    {
        [JsonProperty("ACTIVE_SINCE")]
        public DateTime ActiveSince { get; set; }

        [JsonProperty("TRANSFER_POOL")]
        public int TransferPool { get; set; }

        [JsonProperty("TRANSFER_USED")]
        public int TransferUsed { get; set; }

        [JsonProperty("TRANSFER_BILLABLE")]
        public int TransferBillable { get; set; }
    }
}
