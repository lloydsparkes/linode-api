using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Reference
{
    public class Script
    {
        [JsonProperty("STACKSCRIPTID")]
        public int Id { get; set; }

        [JsonProperty("LABEL")]
        public string Name { get; set; }

        [JsonProperty("CREATE_DT")]
        public DateTime Created { get; set; }

        [JsonProperty("DESCRIPTION")]
        public string Descripton { get; set; }

        [JsonProperty("REV_NOTE")]
        public string RevisionNote { get; set; }

        [JsonProperty("REV_DT")]
        public DateTime RevisionTimestamp { get; set; }

        [JsonProperty("ISPUBLIC")]
        public int IsPublicRaw { get; set; }
        public bool IsPublic { get { return IsPublicRaw == 1; } }

        [JsonProperty("DISTRIBUTIONIDLIST")]
        public string DistributionIdsRaw { get; set; }
        public List<int> DistributionIds
        {
            get
            {
                var list = new List<int>();

                if(string.IsNullOrEmpty(DistributionIdsRaw))
                    return list;

                var split = DistributionIdsRaw.Split(',');
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

        [JsonProperty("LATESTREV")]
        public int LatestRevisionRaw { get; set; }
        public bool LatestRevision { get { return LatestRevisionRaw == 1; } }

        [JsonProperty("DEPLOYMENTSTOTAL")]
        public int DeploymentsActive { get; set; }

        [JsonProperty("USERID")]
        public int UserId { get; set; }

        [JsonProperty("SCRIPT")]
        public string Code { get; set; }
    }
}
