using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using StatusPageIo.Api.Converters;

namespace StatusPageIo.Api.Models.Incidents
{
    [DataContract]
    public class Incident
    {
        [DataMember(Name = "backfilled")]
        public bool Backfilled { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "impact")]
        public IncidentImpact Impact { get; set; }

        [DataMember(Name = "impact_override")]
        public IncidentImpact? ImpactOverride { get; set; }

        [DataMember(Name = "incident_updates")]
        public IList<IncidentUpdate> IncidentUpdates { get; set; }

        [DataMember(Name = "monitoring_at")]
        public DateTime? MonitoringAt { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "page_id")]
        public string PageId { get; set; }

        [DataMember(Name = "postmortem_body")]
        public string PostmortemBody { get; set; }

        [DataMember(Name = "postmortem_body_last_updated_at")]
        public DateTime? PostmortemBodyLastUpdatedAt { get; set; }

        [DataMember(Name = "postmortem_ignored")]
        public bool PostmortemIgnored { get; set; }

        [DataMember(Name = "postmortem_notified_subscribers")]
        public bool PostmortemNotifiedSubscribers { get; set; }

        [DataMember(Name = "postmortem_notified_twitter")]
        public bool PostmortemNotifiedTwitter { get; set; }

        [DataMember(Name = "postmortem_published_at")]
        public DateTime? PostmortemPublishedAt { get; set; }

        [DataMember(Name = "resolved_at")]
        public DateTime? ResolvedAt { get; set; }

        [DataMember(Name = "scheduled_auto_in_progress")]
        public bool ScheduledAutoInProgress { get; set; }

        [DataMember(Name = "scheduled_auto_completed")]
        public bool ScheduledAutoCompleted { get; set; }

        [DataMember(Name = "scheduled_for")]
        public DateTime? ScheduledFor { get; set; }

        [DataMember(Name = "scheduled_remind_prior")]
        public bool ScheduledRemindPrior { get; set; }

        [DataMember(Name = "scheduled_reminded_at")]
        public DateTime? ScheduledRemindedAt { get; set; }

        [DataMember(Name = "scheduled_until")]
        public DateTime? ScheduledUntil { get; set; }

        [DataMember(Name = "shortlink")]
        public string Shortlink { get; set; }

        [DataMember(Name = "status")]
        [JsonConverter(typeof(PascalCaseEnumConverter))]
        public IncidentStatus Status { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
