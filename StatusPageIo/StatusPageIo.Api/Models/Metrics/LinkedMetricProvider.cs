using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StatusPageIo.Api.Models.Metrics
{
    [DataContract]
    public class LinkedMetricProvider
    {
        [DataMember(Name="created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name="disabled")]
        public bool Disabled { get; set; }

        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name="last_revalidated_at")]
        public DateTime LastRevalidatedAt { get; set; }

        [DataMember(Name="page_id")]
        public string PageId { get; set; }

        [DataMember(Name="type")]
        public string Type { get; set; }

        [DataMember(Name="updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
