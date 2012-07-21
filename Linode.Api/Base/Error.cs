using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Base
{
    public class Error
    {
        [JsonProperty("ERRORCODE")]
        public int Code { get; set; }

        [JsonProperty("ERRORMESSAGE")]
        public string Message { get; set; }
    }
}
