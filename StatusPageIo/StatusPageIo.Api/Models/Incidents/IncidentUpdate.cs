using System;
using System.Runtime.Serialization;

namespace StatusPageIo.Api.Models.Incidents
{
    [DataContract]
    public class IncidentUpdate
    {
        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "display_at")]
        public DateTime DisplayAt { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "incident_id")]
        public string IncidentId { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "twitter_updated_at")]
        public DateTime? TwitterUpdatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [DataMember(Name = "wants_twitter_update")]
        public bool WantsTwitterUpdate { get; set; }
    }
}
