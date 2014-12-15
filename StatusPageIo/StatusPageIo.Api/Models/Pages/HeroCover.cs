using System;
using System.Runtime.Serialization;

namespace StatusPageIo.Api.Models.Pages
{
    [DataContract]
    public class HeroCover
    {
        [DataMember(Name = "normal_url")]
        public string NormalUrl { get; set; }

        [DataMember(Name = "original_url")]
        public string OriginalUrl { get; set; }

        [DataMember(Name = "retina_url")]
        public string RetinaUrl { get; set; }

        [DataMember(Name = "size")]
        public int? Size { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
